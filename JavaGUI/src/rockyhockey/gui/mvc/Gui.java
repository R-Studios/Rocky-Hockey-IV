package rockyhockey.gui.mvc;

import java.awt.Color;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;

import rockyhockey.gui.specialbuttons.IconButton;
import rockyhockey.gui.specialbuttons.MuteButton;

public class Gui extends JPanel implements ActionListener {

	private static final long serialVersionUID = 1L;

	private static Gui instance;

	private static ImageIcon playIcon;
	private static ImageIcon resetIcon;
	private static ImageIcon closeIcon;

	private static BufferedImage backgroundImage;

	static {
//		String folder = System.getProperty("user.dir") + "/src/de/rockeyhockey/game/pictures/";
		String folder = "./img/";
		try {
			playIcon = new ImageIcon(ImageIO.read(new File(folder + "play.png")));
			resetIcon = new ImageIcon(ImageIO.read(new File(folder + "replay.png")));
			closeIcon = new ImageIcon(ImageIO.read(new File(folder + "close.png")));
			backgroundImage = ImageIO.read(new File(folder + "background.png"));
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}

	private Color foreground = Color.red;
	private Color foregroundDefault = Color.white;
	private Font font;

	private boolean playPressed;
	private boolean resetPressed;
	private boolean mutePressed;
	
	private boolean soundActive;

	private JLabel playerLabel;
	private JLabel botLabel;
	private JLabel playerScoreLabel;
	private JLabel botScoreLabel;
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

	private Gui() {
		super();
		this.font = new Font("Arial", Font.BOLD, 32);
		initGuiElements();
		setLayout(null);
		addComponents();
		this.soundActive = true;
	}

	private void addComponents() {
		this.add(this.playerLabel);
		this.add(this.botLabel);
		this.add(this.playerScoreLabel);
		this.add(this.botScoreLabel);
		this.add(this.timeLabel);
		this.add(this.playButton);
		this.add(this.resetButton);
		this.add(this.closeButton);
		this.add(this.muteButton);
	}

	@Override
	public void setBounds(int x, int y, int width, int height) {
		super.setBounds(x, y, width, height);
		int eigth_of_with = width / 8;
		int eight_of_height = height / 8;

		this.closeButton.setBounds(width - eigth_of_with , 0, eigth_of_with , eight_of_height);
		this.muteButton.setBounds(width - 2 * eigth_of_with, 0, eigth_of_with, eight_of_height);
		this.playerLabel.setBounds(eigth_of_with, eight_of_height, 2 * eigth_of_with, eight_of_height);
		this.botLabel.setBounds(width - 3 * eigth_of_with, eight_of_height, 2 * eigth_of_with, eight_of_height);
		this.timeLabel.setBounds(3 * eigth_of_with, eight_of_height, 2 * eigth_of_with, eight_of_height);
		this.playerScoreLabel.setBounds(eigth_of_with, 3 * eight_of_height, 2 * eigth_of_with, eight_of_height);
		this.botScoreLabel.setBounds(width - 3 * eigth_of_with, 3 * eight_of_height, 2 * eigth_of_with,
				eight_of_height);
		this.playButton.setBounds(eigth_of_with, 6 * eight_of_height, 2 * eigth_of_with, eight_of_height);
		this.resetButton.setBounds(width - 3 * eigth_of_with, 6 * eight_of_height, 2 * eigth_of_with, eight_of_height);
		JLabel dots = new JLabel(":");
		dots.setFont(font);
		dots.setHorizontalAlignment(JLabel.CENTER);
		dots.setBounds(3 * eigth_of_with, 3 * eight_of_height, 2 * eigth_of_with, eight_of_height);
		dots.setForeground(foregroundDefault);
		this.add(dots);

	}

	private void initGuiElements() {
		this.playButton = new IconButton();
		this.playButton.addActionListener(this);
		this.resetButton = new IconButton();
		this.resetButton.addActionListener(this);
		this.closeButton = new IconButton();
		this.closeButton.addActionListener(this);
		this.muteButton = new MuteButton();
		this.muteButton.addActionListener(this);
		this.playerLabel = new JLabel();
		this.playerLabel.setHorizontalAlignment(JLabel.CENTER);
		this.playerLabel.setVerticalAlignment(JLabel.CENTER);
		this.playerLabel.setForeground(foreground);
		this.playerLabel.setFont(font);
		this.botLabel = new JLabel();
		this.botLabel.setHorizontalAlignment(JLabel.CENTER);
		this.botLabel.setForeground(foreground);
		this.botLabel.setFont(font);
		this.playerScoreLabel = new JLabel();
		this.playerScoreLabel.setHorizontalAlignment(JLabel.CENTER);
		this.playerScoreLabel.setForeground(foregroundDefault);
		this.playerScoreLabel.setFont(font);
		this.botScoreLabel = new JLabel();
		this.botScoreLabel.setHorizontalAlignment(JLabel.CENTER);
		this.botScoreLabel.setForeground(foregroundDefault);
		this.botScoreLabel.setFont(font);
		this.timeLabel = new JLabel();
		this.timeLabel.setHorizontalAlignment(JLabel.CENTER);
		this.timeLabel.setForeground(foregroundDefault);
		this.timeLabel.setFont(font);
		this.reset();

		this.playButton.setIcon(playIcon);
		this.closeButton.setIcon(closeIcon);
		this.resetButton.setIcon(resetIcon);
	}

	public void reset() {
		this.playerLabel.setText("Player");
		this.botLabel.setText("Bot");
		this.playerScoreLabel.setText("0");
		this.botScoreLabel.setText("0");
		this.timeLabel.setText("10:00");
		this.timeLabel.setForeground(foregroundDefault);
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

	@Override
	protected void paintComponent(Graphics g) {
//		super.paintComponent(g);
		g.drawImage(backgroundImage, 0, 0, getBounds().width,getBounds().height,null);
//		g.setColor(Color.black);
//		g.drawRect(0, 0, getBounds().width, getBounds().height);
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
				Audio.enableSound();
			} else {
				Audio.disableSound();
			}
			this.mutePressed = true;
		} else if (sourceButton == this.closeButton) {
			System.exit(0);
		}
	}

}
