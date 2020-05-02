package rockyhockey.gui.specialbuttons;

import javax.swing.ImageIcon;
import javax.swing.JButton;

/**
 * 
 * @author Roman Wecker
 * @version 1.0
 *
 */
public class IconButton extends JButton {

	private static final long serialVersionUID = 1L;

	/**
	 * Creates a new JButton with the icon
	 * @param icon The ImageIcon displayed on the button
	 */
	public IconButton(ImageIcon icon) {
		this.setOpaque(false);
		this.setContentAreaFilled(false);
		this.setBorderPainted(false);
		this.setIcon(icon);
		this.setFocusPainted(false);
	}

	/**
	 * Creates a new JButton without an image
	 */
	public IconButton() {
		this.setOpaque(false);
		this.setContentAreaFilled(false);
		this.setBorderPainted(false);
		this.setFocusPainted(false);
	}

}
