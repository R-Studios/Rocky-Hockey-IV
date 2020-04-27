package rockyhockey.gui.specialbuttons;

import javax.swing.ImageIcon;
import javax.swing.JButton;

public class IconButton extends JButton {

	private static final long serialVersionUID = 1L;

	/*
	 * Constructor with an icon
	 */
	public IconButton(ImageIcon icon) {
		this.setOpaque(false);
		this.setContentAreaFilled(false);
		this.setBorderPainted(false);
		this.setIcon(icon);
		this.setFocusPainted(false);
	}

	/*
	 * Fallback constructor
	 */
	public IconButton() {
		this.setOpaque(false);
		this.setContentAreaFilled(false);
		this.setBorderPainted(false);
		this.setFocusPainted(false);
	}

}
