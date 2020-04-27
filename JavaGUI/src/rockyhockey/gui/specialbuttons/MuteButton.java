package rockyhockey.gui.specialbuttons;

import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JButton;

import rockyhockey.gui.mvc.ResourceLoader;

public class MuteButton extends JButton {

	private static final long serialVersionUID = 6330466439406908672L;

	private static ImageIcon mutedIcon;
	private static ImageIcon unmutedIcon;

	private boolean iconNotNull;

	static {
		try {
			mutedIcon = new ImageIcon(ImageIO.read(ResourceLoader.load("/img/mute.png")));
			unmutedIcon = new ImageIcon(ImageIO.read(ResourceLoader.load("/img/sound.png")));
		}
		catch (IOException e) {
			e.printStackTrace();
		}
	}

	private boolean defaultIcon;

	/*
	 * Constructor
	 */
	public MuteButton() {
		super();
		defaultIcon = true;
		this.setOpaque(false);
		this.setContentAreaFilled(false);
		this.setBorderPainted(false);
		this.setFocusPainted(false);
		this.iconNotNull = mutedIcon != null && unmutedIcon != null;
		if (this.iconNotNull) {
			this.setIcon(unmutedIcon);
		}
	}

	/*
	 * Toggles the displayed icon
	 */
	public void toggleIcon() {
		if (this.iconNotNull) {
			this.defaultIcon ^= true;
			this.setIcon(defaultIcon ? unmutedIcon : mutedIcon);
			this.repaint();
		}
	}

}
