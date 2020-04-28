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
		this.filename = "/sounds/" + filename;
		this.loop = loop;
	}

	/*
	 * (non-Javadoc)
	 * @see java.lang.Thread#run()
	 */
	@Override
	public void run() {
		try {
			soundInputStream = ResourceLoader.load(filename);
			
			InputStream bufferedIn = new BufferedInputStream(soundInputStream);
			
			AudioInputStream inputStream = AudioSystem.getAudioInputStream(bufferedIn);
			
			soundClip = AudioSystem.getClip();
			
			soundClip.open(inputStream);
			
			try {
				synchronized (this) {
					if (loop) {
						soundClip.loop(Clip.LOOP_CONTINUOUSLY);
	
						wait();
					}
					else {
						soundClip.loop(0);
						soundClip.addLineListener(event -> {
							if(LineEvent.Type.STOP.equals(event.getType())) {
								interrupt();
							}
						});
						
						wait(soundClip.getMicrosecondLength() / 1000);
					}
				}
			} 
			catch (InterruptedException e) {
				System.out.println("stopped playing " + filename);
			}

			soundClip.close();
			soundClip.flush();
		}
		catch (Exception e) {
			System.out.println("exception while playing: " + filename);
			System.out.println("exception type: " + e.getClass().getCanonicalName());
			System.out.println("message: " + e.getMessage());
			System.out.println();
		}
	}
}
