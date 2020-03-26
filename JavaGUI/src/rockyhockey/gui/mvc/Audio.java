package rockyhockey.gui.mvc;

import java.applet.Applet;
import java.applet.AudioClip;
import java.io.File;

@SuppressWarnings("deprecation")
public class Audio extends Applet {

	private static final long serialVersionUID = 1L;
	public static final String independentFolder = "./sounds/";
//	public static final String independentFolder = System.getProperty("user.dir") + "/sounds/";

	private static Audio instance;
	
	private static boolean backgroundOn;

	public static AudioClip soundBackground;
	public static AudioClip soundOne;
	public static AudioClip soundTwo;
	public static AudioClip soundThree;
	public static AudioClip soundFour;
	public static AudioClip soundFive;
	public static AudioClip soundSix;
	public static AudioClip soundSeven;
	public static AudioClip soundEight;
	public static AudioClip soundNine;
	public static AudioClip soundTen;
	public static AudioClip soundDominating;
	public static AudioClip soundUnstoppable;
	public static AudioClip soundRampage;
	public static AudioClip soundGodlike;
	public static AudioClip soundTakenlead;
	public static AudioClip soundLostlead;
	public static AudioClip soundPrepare;
	public static AudioClip soundWinner;
	public static AudioClip soundLostmatch;
	private static AudioClip current;
	// private Thread thread;
	private static boolean soundEnabled = true;
	private static boolean soundAvailable = true;

	static {
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
		} catch (Exception e) {
			System.out.println("disabled");
			e.printStackTrace();
			soundAvailable = false;
			soundEnabled = false;
		}
	}

	public AudioClip[] sounds = { soundBackground, soundOne, soundTwo, soundThree, soundFour, soundFive, soundSix,
			soundSeven, soundEight, soundNine, soundTen, soundDominating, soundUnstoppable, soundRampage, soundGodlike,
			soundTakenlead, soundLostlead, soundPrepare, soundWinner, soundLostmatch, current };

	private Audio() {

	}

	public static Audio getInstance() {
		if (instance == null) {
			instance = new Audio();
		}
		return instance;
	}

	public static AudioClip checkAndGetFile(String filename) throws Exception {
		File file = new File(independentFolder + filename);
		
		String filePath = file.getAbsolutePath();

		if (file.exists() && !file.isDirectory()) {
			System.out.println(filePath + " exists");
			
			return newAudioClip(file.toURL());
		} else {
			System.out.println(filePath + " doesn't exist");
			soundAvailable = false;
			soundEnabled = false;
			return null;
		}
	}

	public void playSound(AudioClip sound) {
		if (soundAvailable && soundEnabled) {
			sound.play();
		}
	}

	public void startBackgroundSound() {
		if (soundEnabled == true)
			soundBackground.loop();
			backgroundOn = true;
	}

	public void stopBackgroundSound() {
		if (soundEnabled == true)
			soundBackground.stop();
		
		backgroundOn = false;
	}

	public static void enableSound() {
		if (soundAvailable)
			soundEnabled = true;
		
		if(backgroundOn) {
			soundBackground.loop();
		}
	}

	public static void disableSound() {
		soundEnabled = false;
		soundBackground.stop();
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
