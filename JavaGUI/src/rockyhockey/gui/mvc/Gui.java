package rockyhockey.gui.mvc;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;

import rockyhockey.gui.specialbuttons.IconButton;
import rockyhockey.gui.specialbuttons.MuteButton;

/**
 * 
 * @author Roman Wecker
 * @version 1.0
 *
 */
public class Gui extends JFrame implements ActionListener {

	private static final long serialVersionUID = 1L;

	private static Gui instance;

	private ImageIcon playIcon;
	private ImageIcon resetIcon;
	private ImageIcon closeIcon;

	private Image backgroundImage = null;

	private Color foreground = Color.red;
	private Color foregroundDefault = Color.white;

	private boolean playPressed;
	private boolean resetPressed;
	private boolean mutePressed;

	private boolean soundActive;

	/**
	 * 
	 * @author Roman Wecker
	 * @version 1.0
	 *
	 */
	class PanelWithBackground extends JPanel {
		private static final long serialVersionUID = 1L;

		/**
		 * Draws the background image
		 * @see javax.swing.JComponent#paintComponent(java.awt.Graphics)
		 */
		@Override
		protected void paintComponent(Graphics g) {
			g.clearRect(0, 0, getBounds().width, getBounds().height);
			if (backgroundImage != null)
				g.drawImage(backgroundImage, 0, 0, getBounds().width, getBounds().height, null);
		}
	}

	private PanelWithBackground contentPanel;

	private JLabel playerLabel;
	private JLabel botLabel;
	private JLabel playerScoreLabel;
	private JLabel botScoreLabel;
	private JLabel scoreColon;
	private JLabel timeLabel;

	private IconButton playButton;
	private IconButton resetButton;

	private IconButton closeButton;

	private MuteButton muteButton;

	/**
	 * Creates a single gui instance
	 * @return The single gui instace
	 */
	public static Gui getInstance() {
		if (instance == null) {
			instance = new Gui();
		}
		return instance;
	}

	/**
	 * Creates a new ImageIcon from InputStream
	 * @see ResourceLoader#load(String path)
	 * @param filename The image filename
	 * @return The ImageIcon from the path
	 * @throws Exception Image could not be found
	 */
	private ImageIcon getImageIcon(String filename) throws Exception {
		return new ImageIcon(ImageIO.read(ResourceLoader.load("/img/" + filename)));
	}

	/**
	 * Constructor
	 */
	private Gui() {
		super();

		try {
			playIcon = getImageIcon("play.png");
			resetIcon = getImageIcon("replay.png");
			closeIcon = getImageIcon("close.png");
			ImageIcon backgroundImageIcon = getImageIcon("background.png");
			if (backgroundImageIcon != null)
				backgroundImage = backgroundImageIcon.getImage();
		}
		catch (Exception e) {
			e.printStackTrace();
		}

		initGuiElements();
		addComponents();
		setBounds();

		soundActive = true;

		setVisible(true);
	}

	/**
	 * Adds all gui elements to the JPanel
	 */
	private void addComponents() {
		this.setContentPane(contentPanel);

		contentPanel.add(playerLabel);
		contentPanel.add(botLabel);
		contentPanel.add(playerScoreLabel);
		contentPanel.add(scoreColon);
		contentPanel.add(botScoreLabel);
		contentPanel.add(timeLabel);
		contentPanel.add(playButton);
		contentPanel.add(resetButton);
		contentPanel.add(closeButton);
		contentPanel.add(muteButton);
	}

	/**
	 * Sets the bounds for all gui elements
	 */
	public void setBounds() {
		Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();

		int x = 0;
		int y = 0;
		int width = dim.width;
		int height = dim.height;

		super.setBounds(x, y, width, height);
		setBounds(x, y, width, height);
		contentPanel.setBounds(x, y, width, height);

		int eigth_of_width = width / 8;
		int eight_of_height = height / 8;

		closeButton.setBounds(width - eigth_of_width, 0, eigth_of_width, eight_of_height);
		muteButton.setBounds(width - 2 * eigth_of_width, 0, eigth_of_width, eight_of_height);
		playerLabel.setBounds(eigth_of_width, eight_of_height, 2 * eigth_of_width, eight_of_height);
		botLabel.setBounds(width - 3 * eigth_of_width, eight_of_height, 2 * eigth_of_width, eight_of_height);
		timeLabel.setBounds(3 * eigth_of_width, eight_of_height, 2 * eigth_of_width, eight_of_height);
		playerScoreLabel.setBounds(eigth_of_width, 3 * eight_of_height, 2 * eigth_of_width, eight_of_height);
		botScoreLabel.setBounds(width - 3 * eigth_of_width, 3 * eight_of_height, 2 * eigth_of_width, eight_of_height);
		playButton.setBounds(eigth_of_width, 6 * eight_of_height, 2 * eigth_of_width, eight_of_height);
		resetButton.setBounds(width - 3 * eigth_of_width, 6 * eight_of_height, 2 * eigth_of_width, eight_of_height);
		scoreColon.setBounds(3 * eigth_of_width, 3 * eight_of_height, 2 * eigth_of_width, eight_of_height);
	}

	/**
	 * Creates all gui elements
	 */
	private void initGuiElements() {
		Font font = new Font("Arial", Font.BOLD, 32);

		setLayout(null);
		setUndecorated(true);
		setDefaultCloseOperation(EXIT_ON_CLOSE);

		contentPanel = new PanelWithBackground();
		contentPanel.setLayout(null);

		playButton = new IconButton();
		playButton.addActionListener(this);

		resetButton = new IconButton();
		resetButton.addActionListener(this);

		closeButton = new IconButton();
		closeButton.addActionListener(this);

		muteButton = new MuteButton();
		muteButton.addActionListener(this);

		playerLabel = new JLabel();
		playerLabel.setHorizontalAlignment(JLabel.CENTER);
		playerLabel.setVerticalAlignment(JLabel.CENTER);
		playerLabel.setForeground(foreground);
		playerLabel.setFont(font);

		botLabel = new JLabel();
		botLabel.setHorizontalAlignment(JLabel.CENTER);
		botLabel.setForeground(foreground);
		botLabel.setFont(font);

		playerScoreLabel = new JLabel();
		playerScoreLabel.setHorizontalAlignment(JLabel.CENTER);
		playerScoreLabel.setForeground(foregroundDefault);
		playerScoreLabel.setFont(font);

		scoreColon = new JLabel(":");
		scoreColon.setFont(font);
		scoreColon.setHorizontalAlignment(JLabel.CENTER);
		scoreColon.setForeground(foregroundDefault);

		botScoreLabel = new JLabel();
		botScoreLabel.setHorizontalAlignment(JLabel.CENTER);
		botScoreLabel.setForeground(foregroundDefault);
		botScoreLabel.setFont(font);

		timeLabel = new JLabel();
		timeLabel.setHorizontalAlignment(JLabel.CENTER);
		timeLabel.setForeground(foregroundDefault);
		timeLabel.setFont(font);

		reset();

		playButton.setIcon(playIcon);
		closeButton.setIcon(closeIcon);
		resetButton.setIcon(resetIcon);
	}

	/**
	 * Resets all gui texts and colors
	 */
	public void reset() {
		playerLabel.setText("Player");
		botLabel.setText("Bot");
		playerScoreLabel.setText("0");
		botScoreLabel.setText("0");
		timeLabel.setText("10:00");
		timeLabel.setForeground(foregroundDefault);
		repaint();
	}

	/**
	 * Was the play button pressed
	 * @return Returns if the play button was pressed
	 */
	public boolean isPlayPressed() {
		if (this.playPressed) {
			this.playPressed = false;
			return true;
		}
		return false;
	}

	/**
	 * Was the reset button pressed
	 * @return Returns if the reset button was pressed
	 */
	public boolean isResetPressed() {
		if (this.resetPressed) {
			this.resetPressed = false;
			return true;
		}
		return false;
	}

	/**
	 * Was the mute button pressed
	 * @return Returns if the mute button was pressed
	 */
	public boolean isMutePressed() {
		if (this.mutePressed) {
			this.mutePressed = false;
			return true;
		}
		return false;
	}

	/**
	 * Update the score of the player
	 * @param score The new score of the player
	 */
	public void setPlayerScore(int score) {
		this.playerScoreLabel.setText("" + score);
		this.playerScoreLabel.repaint();
	}

	/**
	 * Update the score of the bot
	 * @param score The new score of the bot
	 */
	public void setBotScore(int score) {
		this.botScoreLabel.setText("" + score);
		this.botScoreLabel.repaint();
	}

	/**
	 * Calculate and update the remaining time
	 * @param countdownTime The time in milliseconds
	 */
	public void setRemainingTime(long countdownTime) {
		int time = (int) (countdownTime / 1000000000);
		int min = time / 60;
		int sec = time % 60;
		this.timeLabel.setText(((min < 10) ? "0" + min : "" + min) + ":" + ((sec < 10) ? "0" + sec : "" + sec));
		if (min < 1) {
			this.timeLabel.setForeground(Color.red);
		}
		this.timeLabel.repaint();
	}

	/**
	 * Handles button events
	 * @see java.awt.event.ActionListener#actionPerformed(java.awt.event.ActionEvent)
	 */
	public void actionPerformed(ActionEvent event) {
		JButton sourceButton = (JButton) event.getSource();
		if (sourceButton == this.playButton) {
			this.playPressed = true;
		}
		else if (sourceButton == this.resetButton) {
			this.resetPressed = true;
		}
		else if (sourceButton == this.muteButton) {
			this.muteButton.toggleIcon();
			soundActive ^= true;
			if (soundActive) {
				Audio.getInstance().enableSound();
			}
			else {
				Audio.getInstance().disableSound();
			}
			this.mutePressed = true;
		}
		else if (sourceButton == this.closeButton) {
			System.exit(0);
		}
	}

}
