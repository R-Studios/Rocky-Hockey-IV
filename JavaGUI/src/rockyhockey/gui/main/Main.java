package rockyhockey.gui.main;

import rockyhockey.gui.mvc.Controller;

public class Main {

	/*
	 * Initialize MVC pattern
	 */
	public static void main(String[] args) {
		Controller controller = Controller.getInstance();
		controller.start();
	}

//	public static void setLs() throws InterruptedException {
//		Scanner sc = new Scanner(System.in);
//		HardwareIO hw = HardwareIO.getInstance();
//		String input = "";
//		while (true) {
//			if (sc.hasNext()) {
//				input = sc.next();
//			}
//
//			if (input.equals("exit")) {
//				break;
//			} else if (input.equals("player")) {
//				hw.setPlayerLs();
//			} else if (input.equals("bot")) {
//				hw.setBotLs();
//			}
//
//			Thread.sleep(5);
//		}
//		sc.close();
//	}

}
