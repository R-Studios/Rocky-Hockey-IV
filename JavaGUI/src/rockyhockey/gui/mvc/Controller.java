package rockyhockey.gui.mvc;

import java.awt.Dimension;
import java.awt.Toolkit;

import javax.swing.JFrame;

public class Controller implements Runnable {

	public static final long GAME_TIME = 600000000000l;
	public static final int PLAYER = 0;
	public static final int BOT = 1;
	public static final int UNDEFINED = -1;
	private static Controller instance;

	private Gui gui;
	private Audio audio;
	private HardwareIO hardware;

	private Controller() {
		this.gui = Gui.getInstance();
		this.audio = Audio.getInstance();
		this.hardware = HardwareIO.getInstance();

		this.initFrame();
	}

	private void initFrame() {
		Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
		JFrame frame = new JFrame();
		frame.setLayout(null);
		frame.setUndecorated(true);
		this.gui.setBounds(0, 0, dim.width, dim.height);
		frame.setBounds(0, 0, dim.width, dim.height);
		frame.add(gui);
		frame.setVisible(true);
	}

	public static Controller getInstance() {
		if (instance == null) {
			instance = new Controller();
		}
		return instance;
	}

	public void start() {
		new Thread(this).start();
	}

	@Override
	public void run() {
		boolean isReseted = true;
		long timeRemaining = GAME_TIME;
		long timeAtStart;
		int scorePlayer = 0;
		int scoreBot = 0;
		int lastGoal = UNDEFINED;
		int highestRun = 0;
		int leader = UNDEFINED;
		try {
			while (true) {
				if (gui.isPlayPressed() && isReseted) {
					isReseted = false;
					audio.playSound(Audio.soundPrepare);
					Thread.sleep(2000);
					audio.startBackgroundSound();
					timeAtStart = System.nanoTime();
					while (timeRemaining > 0) {
						gui.setRemainingTime(timeRemaining);

						if (gui.isResetPressed()) {
							gui.reset();
							isReseted = true;
							break;
						}

						if (hardware.isPlayerLsActive()) {
							scorePlayer++;
							gui.setPlayerScore(scorePlayer);
							if (lastGoal == PLAYER) {
								highestRun++;
							} else {
								highestRun = 1;
								lastGoal = PLAYER;
							}

							if (scorePlayer >= 10) {
								audio.playSound(Audio.soundWinner);
								break;
							} else if (scorePlayer > scoreBot && (leader == BOT || leader == UNDEFINED)) {
								audio.playSound(Audio.soundTakenlead);
								leader = PLAYER;
							} else {
								audio.playScoreSound(highestRun, scorePlayer);
							}
						} else if (hardware.isBotLsActive()) {
							scoreBot++;
							gui.setBotScore(scoreBot);
							if (lastGoal == BOT) {
								highestRun++;
							} else {
								highestRun = 1;
								lastGoal = BOT;
							}
//							playScoreSound(highestRun, scoreBot);
							if (scoreBot >= 10) {
								audio.playSound(Audio.soundLostmatch);
								break;
							} else if (scorePlayer <= scoreBot && leader == PLAYER) {
								audio.playSound(Audio.soundLostlead);
								leader = BOT;
							} else {
								audio.playScoreSound(highestRun, scoreBot); // before scorePlayer right?
							}
						}
						timeRemaining = GAME_TIME - (System.nanoTime() - timeAtStart);
						Thread.sleep(2);
					}
					audio.stopBackgroundSound();
				}
				if (gui.isResetPressed()) {
					gui.reset();
					hardware.resetOutput();
					isReseted = true;
					scoreBot = 0;
					scorePlayer = 0;
				}

				Thread.sleep(2);

			}

		} catch (

		InterruptedException e) {
			e.printStackTrace();
		}

	}
}
