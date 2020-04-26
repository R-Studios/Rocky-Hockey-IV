package rockyhockey.gui.mvc;

public class Audio {

	private static Audio instance;

	private boolean soundEnabled = true;

	private volatile AudioThread backgroundMusicThread = null;

	/*
	 * Constructor
	 */
	private Audio() {

	}

	/*
	 * Get the only audio instance for MVC pattern
	 */
	public static Audio getInstance() {
		if (instance == null) {
			instance = new Audio();
		}
		return instance;
	}

	/*
	 * Play a specific sound
	 */
	public void playSound(String filename) {
		if (soundEnabled) {
			AudioThread.playSound(filename);			
		}
	}

	/*
	 * Start the background track
	 */
	public void startBackgroundSound() {
		if (soundEnabled) {
			if (backgroundMusicThread == null) {
				backgroundMusicThread = AudioThread.playSound("backgroundsound.wav", true);
			}			
		}
	}

	/*
	 * Stop the background track
	 */
	public void stopBackgroundSound() {
		if (backgroundMusicThread != null) {
			synchronized (backgroundMusicThread) {
				backgroundMusicThread.interrupt();
				
				backgroundMusicThread = null;
			}
		}
	}

	/*
	 * Event sound toggle
	 */
	public void enableSound() {
		soundEnabled = true;

		startBackgroundSound();
	}

	/*
	 * Event sound toggle
	 */
	public void disableSound() {
		soundEnabled = false;

		stopBackgroundSound();
	}

	/*
	 * Play sound for streaks
	 */
	public void playScoreSound(int run, int score) {
		switch (run) {
		case 3:
			playSound("dominating.wav");
			break;
		case 5:
			playSound("rampage.wav");
			break;
		case 7:
			playSound("unstoppable.wav");
			break;
		case 9:
			playSound("godlike.wav");
			break;
		default:
			playGoalSound(score);
			break;
		}
	}

	/*
	 * Play sound for a goal
	 */
	public void playGoalSound(int goal) {
		switch (goal) {
		case 1:
			playSound("one.wav");
			break;
		case 2:
			playSound("two.wav");
			break;
		case 3:
			playSound("three.wav");
			break;
		case 4:
			playSound("four.wav");
			break;
		case 5:
			playSound("five.wav");
			break;
		case 6:
			playSound("six.wav");
			break;
		case 7:
			playSound("seven.wav");
			break;
		case 8:
			playSound("eight.wav");
			break;
		case 9:
			playSound("nine.wav");
			break;
		case 10:
			playSound("ten.wav");
			break;
		}
	}
}
