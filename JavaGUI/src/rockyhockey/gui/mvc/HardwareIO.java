package rockyhockey.gui.mvc;

import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;

/**
 * 
 * @author Roman Wecker
 * @version 1.0
 *
 */
public class HardwareIO implements Runnable {

	private static HardwareIO instance;

	private static final String GPIO_DIRECTORY = "/sys/class/gpio/";
	private volatile boolean playerLs;
	private volatile boolean botLs;

	/**
	 * Creates a single hardware io instance
	 * @return The single hardware io instance
	 */
	public static HardwareIO getInstance() {
		if (instance == null) {
			instance = new HardwareIO();
		}
		return instance;
	}

	/**
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

	/**
	 * Initializes GPIO pins as input
	 * @param gpio The GPIO pin on the raspberry pi
	 * @return Returns true if GPIO pins exist
	 * @throws IOException The GPIO pins could not be found
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

	/**
	 * Read a signal from a specific GPIO pin
	 * @param gpio The GPIO pin on the raspberry pi
	 * @return Returns signal on GPIO pin
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

	/**
	 * Resets the output
	 */
	public void resetOutput() {
		playerLs = false;
		botLs = false;
	}

	/**
	 * Is the player active
	 * @return Returns if the player is active
	 */
	public boolean isPlayerLsActive() {
		if (playerLs) {
			playerLs = false;
			return true;
		}
		return false;
	}

	/**
	 * Is the bot active
	 * @return Returns if the bot is active
	 */
	public boolean isBotLsActive() {
		if (botLs) {
			botLs = false;
			return true;
		}
		return false;
	}

	/**
	 * Sets the player active
	 */
	public void setPlayerLs() {
		playerLs = true;
	}

	/**
	 * Sets the bot active
	 */
	public void setBotLs() {
		botLs = true;
	}

	/**
	 * GPIO read process
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
