package rockyhockey.gui.specialbuttons;

import javax.swing.ImageIcon;
import javax.swing.JButton;

public class IconButton extends JButton{

	private static final long serialVersionUID = 1L;

	public IconButton(ImageIcon icon) {
		this.setOpaque(false);
		this.setContentAreaFilled(false);
		this.setBorderPainted(false);
		this.setIcon(icon);
	}
	
	public IconButton() {
		this.setOpaque(false);
		this.setContentAreaFilled(false);
		this.setBorderPainted(false);
		
	}

}
