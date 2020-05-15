package rockyhockey.gui.mvc;

/**
 * 
 * @author Roman Wecker
 * @version 1.0
 *
 */
public class Audio {

	private static Audio instance;
	private boolean soundEnabled = true;
	private volatile AudioThread backgroundMusicThread = null;

	/**
	 * Creates a single audio instance
	 * @return The single audio instance
	 */
	public static Audio getInstance() {
		if (instance == null) {
			instance = new Audio();
		}
		return instance;
	}

	/**
	 * Creates a new Thread and plays the sound effect
	 * @see AudioThread#playSound(String filename)
	 * @param filename The audio filename
	 */
	public void playSound(String filename) {
		if (soundEnabled) {
			AudioThread.playSound(filename);			
		}
	}

	/**
	 * Creates a new Thread and plays the background music
	 * @see AudioThread#playSound(String filename)
	 */
	public void startBackgroundSound() {
		if (soundEnabled) {
			if (backgroundMusicThread == null) {
				backgroundMusicThread = AudioThread.playSound("backgroundsound.wav", true);
			}			
		}
	}

	/**
	 * Stops the background music thread
	 */
	public void stopBackgroundSound() {
		if (backgroundMusicThread != null) {
			synchronized (backgroundMusicThread) {
				backgroundMusicThread.interrupt();
				
				backgroundMusicThread = null;
			}
		}
	}

	/**
	 * Enables sounds
	 */
	public void enableSound() {
		soundEnabled = true;
		startBackgroundSound();
	}

	/**
	 * Disables sounds
	 */
	public void disableSound() {
		soundEnabled = false;
		stopBackgroundSound();
	}

	/**
	 * Plays a sound for a streak of goals
	 * @see#playSound(String filename)
	 * @see#playGoalSound(int goal)
	 * @param run The current wins in a row
	 * @param score The current goals of the player or bot (used as fallback)
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

	/**
	 * Plays a sound for a goal
	 * @param goal The current goals of the player or bot
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
