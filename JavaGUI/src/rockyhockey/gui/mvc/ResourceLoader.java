package rockyhockey.gui.mvc;

import java.io.InputStream;

/**
 * 
 * @author Roman Wecker
 * @version 1.0
 *
 */
final public class ResourceLoader {
	
	/**
	 * Load resources like images or audio as input streams
	 * @param path The relative path to the resource location
	 * @return Returns the resource as InputStream
	 */
	public static InputStream load(String path) {
		InputStream input = ResourceLoader.class.getResourceAsStream(path);
		if (input == null) {
			input = ResourceLoader.class.getResourceAsStream("/" + path);
		}
		return input;
	}
	
}
