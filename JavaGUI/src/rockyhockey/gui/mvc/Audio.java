package rockyhockey.gui.mvc;

import java.io.File;
import java.net.MalformedURLException;

public class Audio {

	public static final String independentFolder = "./sounds/";

	private static Audio instance;

	public File soundBackground;
	public File soundOne;
	public File soundTwo;
	public File soundThree;
	public File soundFour;
	public File soundFive;
	public File soundSix;
	public File soundSeven;
	public File soundEight;
	public File soundNine;
	public File soundTen;
	public File soundDominating;
	public File soundUnstoppable;
	public File soundRampage;
	public File soundGodlike;
	public File soundTakenlead;
	public File soundLostlead;
	public File soundPrepare;
	public File soundWinner;
	public File soundLostmatch;

	private boolean soundAvailable = true;
	private boolean soundEnabled = true;
	private boolean backgroundEnabled = true;

	private volatile AudioThread backgroundMusicThread = null;

	public File[] sounds = { soundBackground, soundOne, soundTwo, soundThree, soundFour, soundFive, soundSix,
			soundSeven, soundEight, soundNine, soundTen, soundDominating, soundUnstoppable, soundRampage, soundGodlike,
			soundTakenlead, soundLostlead, soundPrepare, soundWinner, soundLostmatch };

	private Audio() {
		try {
			soundBackground = checkAndGetFile("backgroundsound.wav");
			soundOne = checkAndGetFile("one.wav");
			soundTwo = checkAndGetFile("two.wav");
			soundThree = checkAndGetFile("three.wav");
			soundFour = checkAndGetFile("four.wav");
			soundFive = checkAndGetFile("five.wav");
			soundSix = checkAndGetFile("six.wav");
			soundSeven = checkAndGetFile("seven.wav");
			soundEight = checkAndGetFile("eight.wav");
			soundNine = checkAndGetFile("nine.wav");
			soundTen = checkAndGetFile("ten.wav");
			soundDominating = checkAndGetFile("dominating.wav");
			soundUnstoppable = checkAndGetFile("unstoppable.wav");
			soundRampage = checkAndGetFile("rampage.wav");
			soundGodlike = checkAndGetFile("godlike.wav");
			soundTakenlead = checkAndGetFile("takenlead.wav");
			soundLostlead = checkAndGetFile("lostlead.wav");
			soundPrepare = checkAndGetFile("prepare.wav");
			soundWinner = checkAndGetFile("winner.wav");
			soundLostmatch = checkAndGetFile("lostmatch.wav");
		}
		catch (Exception e) {
			System.out.println("disabled");
			e.printStackTrace();
			soundAvailable = false;
			soundEnabled = false;
		}
	}

	public static Audio getInstance() {
		if (instance == null) {
			instance = new Audio();
		}
		return instance;
	}

	private File checkAndGetFile(String filename) throws MalformedURLException {
		File soundFile = new File(independentFolder + filename);

		String soundFilePath = soundFile.getAbsolutePath();

		if (soundFile.exists() && !soundFile.isDirectory()) {
			System.out.println(soundFilePath + " exists");

			return soundFile;
		}

		System.out.println(soundFilePath + " doesn't exist");
		soundAvailable = false;
		soundEnabled = false;
		return null;
	}

	public void playSound(File soundFile) {
		if (soundAvailable && soundEnabled)
			AudioThread.playSound(soundFile);
	}

	public void startBackgroundSound() {
		if (soundAvailable) {
			if (backgroundMusicThread == null) {
				backgroundMusicThread = AudioThread.playSound(soundBackground, true);				
			}
		}

		// backgroundEnabled = true;
	}

	public void stopBackgroundSound() {
		synchronized (backgroundMusicThread) {
			if (backgroundMusicThread != null) {
				backgroundMusicThread.interrupt();
				
				backgroundMusicThread = null;
			}
		}

		// backgroundEnabled = false;
	}

	public void enableSound() {
		if (soundAvailable) {
			soundEnabled = true;

			startBackgroundSound();
		}
	}

	public void disableSound() {
		soundEnabled = false;

		stopBackgroundSound();
	}

	public void playScoreSound(int run, int score) {
		switch (run) {
		case 3:
			playSound(soundDominating);
			break;
		case 5:
			playSound(soundRampage);
			break;
		case 7:
			playSound(soundUnstoppable);
			break;
		case 9:
			playSound(soundGodlike);
			break;
		default:
			playGoalSound(score);
			break;
		}
	}

	public void playGoalSound(int goal) {
		switch (goal) {
		case 1:
			playSound(soundOne);
			break;
		case 2:
			playSound(soundTwo);
			break;
		case 3:
			playSound(soundThree);
			break;
		case 4:
			playSound(soundFour);
			break;
		case 5:
			playSound(soundFive);
			break;
		case 6:
			playSound(soundSix);
			break;
		case 7:
			playSound(soundSeven);
			break;
		case 8:
			playSound(soundEight);
			break;
		case 9:
			playSound(soundNine);
			break;
		case 10:
			playSound(soundTen);
			break;
		}
	}
}
