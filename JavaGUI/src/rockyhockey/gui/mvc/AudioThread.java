package rockyhockey.gui.mvc;

import java.io.File;

import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;
import javax.sound.sampled.Clip;

public class AudioThread extends Thread implements Runnable {
	
	private File soundFile;
	private boolean loop;

	public static AudioThread playSound(File soundFile, boolean loop) {
		if (soundFile != null && soundFile.exists() && !soundFile.isDirectory()) {
			AudioThread soundThread = new AudioThread(soundFile, loop);
			soundThread.start();

			return soundThread;
		}

		return null;
	}

	public static AudioThread playSound(File soundFile) {
		return playSound(soundFile, false);
	}

	private AudioThread(File soundFile, boolean loop) {
		this.soundFile = soundFile;
		this.loop = loop;
	}

	@Override
	public void run() {
		try {
			do {
				AudioInputStream inputStream = AudioSystem.getAudioInputStream(soundFile);

				Clip soundClip = AudioSystem.getClip();

				soundClip.open(inputStream);

				long clipDuration = soundClip.getMicrosecondLength() / 1000;

				try {
					synchronized (this) {
						soundClip.start();

						wait(clipDuration);
					}
				} catch (InterruptedException e) {
					soundClip.stop();

					loop = false;

					System.out.println("stopped playing " + soundFile.getAbsolutePath());
				}

				soundClip.close();
			}
			while (loop);
		}
		catch (Exception e) {
			System.out.println("exception while playing file: " + soundFile.getAbsolutePath());
			System.out.println("exception type: " + e.getClass().getCanonicalName());
			System.out.println("message: " + e.getMessage());
			System.out.println();
		}
	}
}
