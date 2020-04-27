package rockyhockey.gui.mvc;

import java.io.InputStream;

final public class ResourceLoader {

	/*
	 * Load resources such as images or audio as inputstreams
	 */
	public static InputStream load(String path) {
		InputStream input = ResourceLoader.class.getResourceAsStream(path);
		if (input == null) {
			input = ResourceLoader.class.getResourceAsStream("/" + path);
		}
		return input;
	}
	
}
