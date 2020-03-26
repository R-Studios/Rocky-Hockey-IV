package rockyhockey.gui.specialbuttons;

import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JButton;

public class MuteButton extends JButton {

	private static final long serialVersionUID = 6330466439406908672L;

	private static ImageIcon mutedIcon;
	private static ImageIcon unmutedIcon;

	private boolean iconNotNull;

	static {
		try {
			//String folder = System.getProperty("user.dir") + "/src/de/rockeyhockey/game/pictures/";
			String folder = "./img/";
			mutedIcon = new ImageIcon(ImageIO.read(new File(folder + "mute.png")));
			unmutedIcon = new ImageIcon(ImageIO.read(new File(folder + "sound.png")));
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	private boolean defaultIcon;

	public MuteButton() {
		super();
		defaultIcon = true;
		this.setOpaque(false);
		this.setContentAreaFilled(false);
		this.setBorderPainted(false);
		this.iconNotNull = mutedIcon != null && unmutedIcon != null;
		if (this.iconNotNull) {
			this.setIcon(unmutedIcon);
		}
	}

	public void toggleIcon() {
		if (this.iconNotNull) {
			this.defaultIcon ^= true;
			this.setIcon(defaultIcon ? unmutedIcon : mutedIcon);
			this.repaint();
		}
	}

}
