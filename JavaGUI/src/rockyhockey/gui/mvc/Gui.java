package rockyhockey.gui.mvc;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;

import rockyhockey.gui.specialbuttons.IconButton;
import rockyhockey.gui.specialbuttons.MuteButton;

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
	
	class PanelWithBackground extends JPanel {
		private static final long serialVersionUID = -3597732424273848852L;

		@Override
		protected void paintComponent(Graphics g) {
			if (backgroundImage != null)
				g.drawImage(backgroundImage, 0, 0, getBounds().width,getBounds().height,null);
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

	public static Gui getInstance() {
		if (instance == null) {
			instance = new Gui();
		}
		return instance;
	}

	private ImageIcon checkAndGetFile(String filename) throws Exception {
		File imageFile = new File("./img/" + filename);
		
		String imageFilePath = imageFile.getAbsolutePath();

		if (imageFile.exists() && !imageFile.isDirectory()) {
			System.out.println(imageFilePath + " exists");

			return new ImageIcon(ImageIO.read(imageFile));
		}

		System.out.println(imageFilePath + " doesn't exist");
		return null;
	}

	private Gui() {
		super();
		
		try {
			playIcon = checkAndGetFile("play.png");
			resetIcon = checkAndGetFile("replay.png");
			closeIcon = checkAndGetFile("close.png");
			ImageIcon backgroundImageIcon = checkAndGetFile("ggrafikk.png");
			if (backgroundImageIcon != null)
				backgroundImage = backgroundImageIcon.getImage();
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		initGuiElements();
		addComponents();
		setBounds();
		
		soundActive = true;

		setVisible(true);
	}

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

	public void setBounds() {
		Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
		
		int x = 0;
		int y = 0;
		int width = dim.width;
		int height = dim.height;

		super.setBounds(x, y, width, height);
		setBounds(x, y, width, height);
		contentPanel.setBounds(x, y, width, height);
		
		int eigth_of_with = width / 8;
		int eight_of_height = height / 8;

		closeButton.setBounds(width - eigth_of_with , 0, eigth_of_with , eight_of_height);
		muteButton.setBounds(width - 2 * eigth_of_with, 0, eigth_of_with, eight_of_height);
		playerLabel.setBounds(eigth_of_with, eight_of_height, 2 * eigth_of_with, eight_of_height);
		botLabel.setBounds(width - 3 * eigth_of_with, eight_of_height, 2 * eigth_of_with, eight_of_height);
		timeLabel.setBounds(3 * eigth_of_with, eight_of_height, 2 * eigth_of_with, eight_of_height);
		playerScoreLabel.setBounds(eigth_of_with, 3 * eight_of_height, 2 * eigth_of_with, eight_of_height);
		botScoreLabel.setBounds(width - 3 * eigth_of_with, 3 * eight_of_height, 2 * eigth_of_with, eight_of_height);
		playButton.setBounds(eigth_of_with, 6 * eight_of_height, 2 * eigth_of_with, eight_of_height);
		resetButton.setBounds(width - 3 * eigth_of_with, 6 * eight_of_height, 2 * eigth_of_with, eight_of_height);
		scoreColon.setBounds(3 * eigth_of_with, 3 * eight_of_height, 2 * eigth_of_with, eight_of_height);

	}

	private void initGuiElements() {
		Font font = new Font("Arial", Font.BOLD, 32);
		
		setLayout(null);
		setUndecorated(true);
		
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

	public void reset() {
		playerLabel.setText("Player");
		botLabel.setText("Bot");
		playerScoreLabel.setText("0");
		botScoreLabel.setText("0");
		timeLabel.setText("10:00");
		timeLabel.setForeground(foregroundDefault);
		repaint();
	}

	public boolean isPlayPressed() {
		if (this.playPressed) {
			this.playPressed = false;
			return true;
		}
		return false;
	}

	public boolean isResetPressed() {
		if (this.resetPressed) {
			this.resetPressed = false;
			return true;
		}
		return false;
	}

	public boolean isMutePressed() {
		if (this.mutePressed) {
			this.mutePressed = false;
			return true;
		}
		return false;
	}

	public void setPlayerScore(int score) {
		this.playerScoreLabel.setText("" + score);
		this.playerScoreLabel.repaint();
	}

	public void setBotScore(int score) {
		this.botScoreLabel.setText("" + score);
		this.botScoreLabel.repaint();
	}

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

	public void actionPerformed(ActionEvent event) {
		JButton sourceButton = (JButton) event.getSource();
		if (sourceButton == this.playButton) {
			this.playPressed = true;
		} else if (sourceButton == this.resetButton) {
			this.resetPressed = true;
		} else if (sourceButton == this.muteButton) {
			this.muteButton.toggleIcon();
			soundActive ^= true;
			if(soundActive) {
				Audio.getInstance().enableSound();
			} else {
				Audio.getInstance().disableSound();
			}
			this.mutePressed = true;
		} else if (sourceButton == this.closeButton) {
			System.exit(0);
		}
	}

}
