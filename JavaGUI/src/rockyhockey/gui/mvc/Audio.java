package rockyhockey.gui.mvc;

import java.applet.Applet;
import java.applet.AudioClip;
import java.io.File;

@SuppressWarnings("deprecation")
public class Audio extends Applet {

	private static final long serialVersionUID = 1L;
//	public static final String independentFolder = System.getProperty("user.dir") + "/sounds/";

	private static Audio instance;
	
	private boolean backgroundOn;

	public AudioClip soundBackground;
	public AudioClip soundOne;
	public AudioClip soundTwo;
	public AudioClip soundThree;
	public AudioClip soundFour;
	public AudioClip soundFive;
	public AudioClip soundSix;
	public AudioClip soundSeven;
	public AudioClip soundEight;
	public AudioClip soundNine;
	public AudioClip soundTen;
	public AudioClip soundDominating;
	public AudioClip soundUnstoppable;
	public AudioClip soundRampage;
	public AudioClip soundGodlike;
	public AudioClip soundTakenlead;
	public AudioClip soundLostlead;
	public AudioClip soundPrepare;
	public AudioClip soundWinner;
	public AudioClip soundLostmatch;
	private AudioClip current;
	// private Thread thread;
	private boolean soundEnabled = true;
	private boolean soundAvailable = true;

	public AudioClip[] sounds = { soundBackground, soundOne, soundTwo, soundThree, soundFour, soundFive, soundSix,
			soundSeven, soundEight, soundNine, soundTen, soundDominating, soundUnstoppable, soundRampage, soundGodlike,
			soundTakenlead, soundLostlead, soundPrepare, soundWinner, soundLostmatch, current };

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
		} catch (Exception e) {
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

	public AudioClip checkAndGetFile(String filename) throws Exception {
		String soundFolder = "./sounds/";
		
		File file = new File(soundFolder + filename);
		
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

	public void enableSound() {
		if (soundAvailable)
			soundEnabled = true;
		
		if(backgroundOn) {
			soundBackground.loop();
		}
	}

	public void disableSound() {
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
