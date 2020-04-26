package rockyhockey.gui.mvc;


import java.io.BufferedInputStream;
import java.io.InputStream;

import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;
import javax.sound.sampled.Clip;
import javax.sound.sampled.LineEvent;

public class AudioThread extends Thread implements Runnable {
	
	private Clip soundClip;
	private InputStream soundInputStream;
	private String filename;
	private boolean loop;

	/*
	 * Create a new AudioThread for each sound
	 */
	public static AudioThread playSound(String filename, boolean loop) {
		AudioThread soundThread = new AudioThread(filename, loop);
		soundThread.start();

		return soundThread;
	}

	/*
	 * Play a specific sound
	 */
	public static AudioThread playSound(String filename) {
		return playSound(filename, false);
	}

	/*
	 * Constructor
	 */
	private AudioThread(String filename, boolean loop) {
		this.filename = filename;
		this.loop = loop;
	}

	/*
	 * (non-Javadoc)
	 * @see java.lang.Thread#run()
	 */
	@Override
	public void run() {
		try {
			
			soundInputStream = ResourceLoader.load("/sounds/" + filename);
			
			InputStream bufferedIn = new BufferedInputStream(soundInputStream);
			
			AudioInputStream inputStream = AudioSystem.getAudioInputStream(bufferedIn);
			
			soundClip = AudioSystem.getClip();
			
			soundClip.open(inputStream);
			
			if (loop) {
				soundClip.loop(Clip.LOOP_CONTINUOUSLY);
			}
			else {
				soundClip.loop(0);
				soundClip.addLineListener(event -> {
					if(LineEvent.Type.STOP.equals(event.getType())) {
						soundClip.close();
						soundClip.flush();
						interrupt();
					}
				});
			}  
			
			while (true) {
				if (Thread.interrupted()) {
					soundClip.close();
					soundClip.flush();
					interrupt();
				}
			}
		}
		catch (Exception e) {
			//System.out.println("exception while playing file: " + soundFile.getAbsolutePath());
			System.out.println("exception type: " + e.getClass().getCanonicalName());
			System.out.println("message: " + e.getMessage());
			System.out.println();
		}
	}
}
