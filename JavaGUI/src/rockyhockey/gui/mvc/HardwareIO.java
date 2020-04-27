package rockyhockey.gui.mvc;

import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;

public class HardwareIO implements Runnable {

	private static HardwareIO instance;

	private static final String GPIO_DIRECTORY = "/sys/class/gpio/";
	private volatile boolean playerLs;
	private volatile boolean botLs;

	/*
	 * Get the hardwareio instance
	 */
	public static HardwareIO getInstance() {
		if (instance == null) {
			instance = new HardwareIO();
		}
		return instance;
	}

	/*
	 * Constructor
	 */
	private HardwareIO() {
		try {
			if (!initGPIOasInput(5) || !initGPIOasInput(6)) {
				System.err.println("gpio fail");
			}

			Thread thread = new Thread(this);
			thread.start();
		}
		catch (IOException e) {
			e.printStackTrace();
		}

	}

	/*
	 * Initialize gpio pin as input
	 */
	public boolean initGPIOasInput(int gpio) throws IOException {
		File gpioLocation = new File(GPIO_DIRECTORY + "gpio" + gpio + "/value");
		if (gpioLocation.exists()) {
			return true;
		}
		FileWriter fw = new FileWriter(new File(GPIO_DIRECTORY + "export"));
		fw.write("" + gpio);
		fw.close();
		return gpioLocation.exists();
	}

	/*
	 * Read signal from a specific gpio pin
	 */
	public boolean readGPIOSignal(int gpio) {
		try {
			FileReader fr = new FileReader(new File(GPIO_DIRECTORY + "gpio" + gpio + "/value"));
			char[] fileOut = new char[1];
			fr.read(fileOut);
			fr.close();
			return fileOut[0] == '1';
		}
		catch (IOException e) {
			System.err.println("gpio fail");
			e.printStackTrace();
		}
		return false;
	}

	/*
	 * Reset the output
	 */
	public void resetOutput() {
		playerLs = false;
		botLs = false;
	}

	/*
	 * Returns if player is active
	 */
	public boolean isPlayerLsActive() {
		if (playerLs) {
			playerLs = false;
			return true;
		}
		return false;
	}

	/*
	 * Returns if bot is active
	 */
	public boolean isBotLsActive() {
		if (botLs) {
			botLs = false;
			return true;
		}
		return false;
	}

	/*
	 * Set the player active
	 */
	public void setPlayerLs() {
		playerLs = true;
	}

	/*
	 * Set the bot active
	 */
	public void setBotLs() {
		botLs = true;
	}

	/*
	 * (non-Javadoc)
	 * @see java.lang.Runnable#run()
	 */
	@Override
	public void run() {
		try {
			long timeStemp = 0;
			long currentTime = 0;
			boolean playerWasHigh = false;
			boolean botWasHigh = false;
			while (true) {
				boolean gpio5 = readGPIOSignal(5);
				boolean gpio6 = readGPIOSignal(6);
				if (!playerWasHigh && gpio5) {
					if ((currentTime = System.currentTimeMillis()) - timeStemp > 2000) {
						playerWasHigh = true;
						playerLs = true;
						timeStemp = currentTime;
					}

				}
				else if (!gpio5) {
					playerWasHigh = false;
				}
				if (!botWasHigh && gpio6) {
					if ((currentTime = System.currentTimeMillis()) - timeStemp > 2000) {
						botWasHigh = true;
						botLs = true;
						timeStemp = currentTime;
					}
				}
				else if (!gpio6) {
					botWasHigh = false;
				}
				Thread.sleep(10);
			}
		}
		catch (InterruptedException e) {
			e.printStackTrace();
		}
	}

}
