package rockyhockey.gui.mvc;


import java.io.BufferedInputStream;
import java.io.InputStream;

import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;
import javax.sound.sampled.Clip;
import javax.sound.sampled.LineEvent;

/**
 * 
 * @author Roman Wecker
 * @version 1.0
 *
 */
public class AudioThread extends Thread implements Runnable {
	
	private Clip soundClip;
	private InputStream soundInputStream;
	private String filename;
	private boolean loop;

	/**
	 * Creates a new AudioThread for the sound
	 * @param filename The audio filename
	 * @param loop Should the sound be looped
	 * @return Returns the new AudioThread for the sound
	 */
	public static AudioThread playSound(String filename, boolean loop) {
		AudioThread soundThread = new AudioThread(filename, loop);
		soundThread.start();
		return soundThread;
	}

	/**
	 * Redirects method call for the default parameter
	 * @param filename The audio filename
	 * @return Returns method call for the default parameter
	 */
	public static AudioThread playSound(String filename) {
		return playSound(filename, false);
	}

	/**
	 * Constructor
	 * @param filename The audio filename
	 * @param loop Should the sound be looped
	 */
	private AudioThread(String filename, boolean loop) {
		this.filename = "/sounds/" + filename;
		this.loop = loop;
	}


	/**
	 * Plays the sound until it ends or is interrupted
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
