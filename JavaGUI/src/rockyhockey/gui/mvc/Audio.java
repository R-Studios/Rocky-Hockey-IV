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
	private static boolean soundNotAvailable = false;

	static {
		try {
			checkFile("backgroundsound.wav");
			checkFile("one.wav");
			checkFile("two.wav");
			checkFile("three.wav");
			checkFile("four.wav");
			checkFile("five.wav");
			checkFile("six.wav");
			checkFile("seven.wav");
			checkFile("eight.wav");
			checkFile("nine.wav");
			checkFile("ten.wav");
			checkFile("dominating.wav");
			checkFile("unstoppable.wav");
			checkFile("rampage.wav");
			checkFile("godlike.wav");
			checkFile("takenlead.wav");
			checkFile("lostlead.wav");
			checkFile("prepare.wav");
			checkFile("winner.wav");
			checkFile("lostmatch.wav");
			soundBackground = newAudioClip((new File(independentFolder + "backgroundsound.wav")).toURL());
			soundOne = newAudioClip((new File(independentFolder + "one.wav")).toURL());
			soundTwo = newAudioClip((new File(independentFolder + "two.wav")).toURL());
			soundThree = newAudioClip((new File(independentFolder + "three.wav")).toURL());
			soundFour = newAudioClip((new File(independentFolder + "four.wav")).toURL());
			soundFive = newAudioClip((new File(independentFolder + "five.wav")).toURL());
			soundSix = newAudioClip((new File(independentFolder + "six.wav")).toURL());
			soundSeven = newAudioClip((new File(independentFolder + "seven.wav")).toURL());
			soundEight = newAudioClip((new File(independentFolder + "eight.wav")).toURL());
			soundNine = newAudioClip((new File(independentFolder + "nine.wav")).toURL());
			soundTen = newAudioClip((new File(independentFolder + "ten.wav")).toURL());
			soundDominating = newAudioClip((new File(independentFolder + "dominating.wav")).toURL());
			soundUnstoppable = newAudioClip((new File(independentFolder + "unstoppable.wav")).toURL());
			soundRampage = newAudioClip((new File(independentFolder + "rampage.wav")).toURL());
			soundGodlike = newAudioClip((new File(independentFolder + "godlike.wav")).toURL());
			soundTakenlead = newAudioClip((new File(independentFolder + "takenlead.wav")).toURL());
			soundLostlead = newAudioClip((new File(independentFolder + "lostlead.wav")).toURL());
			soundPrepare = newAudioClip((new File(independentFolder + "prepare.wav")).toURL());
			soundWinner = newAudioClip((new File(independentFolder + "winner.wav")).toURL());
			soundLostmatch = newAudioClip((new File(independentFolder + "lostmatch.wav")).toURL());
		} catch (Exception e) {
			System.out.println("disabled");
			e.printStackTrace();
			soundNotAvailable = true;
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

	public static void checkFile(String filename) {
		File f = new File(independentFolder + filename);

		if (f.exists() && !f.isDirectory()) {
			System.out.println(f.getAbsolutePath() + " exists");
		} else {
			System.out.println(f.getAbsolutePath() + " don't exists");
			soundNotAvailable = true;
			soundEnabled = false;
		}
	}

	public void playSound(AudioClip sound) {
		if (soundEnabled) {
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
		if (soundNotAvailable == false)
			soundEnabled = true;
		
		if(backgroundOn) {
			soundBackground.loop();
		}
	}

	public static void disableSound() {
		soundEnabled = false;
		soundBackground.stop();
	}

}
