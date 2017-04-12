import java.io.*;
import java.net.*;
import java.util.Random;
import java.util.logging.*;



import com.sun.net.httpserver.*;


// TODO: Auto-generated Javadoc
/**
 * The Class Server.
 */
@SuppressWarnings("restriction")
public class Server {

	/** The server_port_number. */
	//private static int server_port_number = 3031;
	
	/** The Constant MAX_WAITING_CONNECTIONS. */
	private static final int MAX_WAITING_CONNECTIONS = 10;
	
	public static final String HOST = "localhost";
	
	/** The logger. */
	private static Logger logger;	
	
	/** The server. */
	private HttpServer server;
	
	Random rand = new Random();
	
	static {
		try {
			initLog();
	}
		catch (IOException e) {
			System.out.println("Could not initialize log: " + e.getMessage());
		}
	}
	
	/**
	 * Inits the log.
	 *
	 * @throws IOException Signals that an I/O exception has occurred.
	 */
	private static void initLog() throws IOException {
		
		Level logLevel = Level.FINE;
		
		logger = Logger.getLogger("Test"); 
		logger.setLevel(logLevel);
		logger.setUseParentHandlers(false);
		
		Handler consoleHandler = new ConsoleHandler();
		consoleHandler.setLevel(logLevel);
		consoleHandler.setFormatter(new SimpleFormatter());
		logger.addHandler(consoleHandler);

		FileHandler fileHandler = new FileHandler("log.txt", false);
		fileHandler.setLevel(logLevel);
		fileHandler.setFormatter(new SimpleFormatter());
		logger.addHandler(fileHandler);
	}
	
	private int port;
		
	/**
	 * Instantiates a new server.
	 */
	public Server(int port){
		this.port = port;
		return;
	}
	
	/**
	 * Run.
	 * @throws InterruptedException 
	 */
	public void run() throws InterruptedException{
		logger.info("Initializing Model");		
		logger.info("Initializing HTTP Server on port " + this.port);
	
		try {
			server = HttpServer.create(new InetSocketAddress(this.port), 
										MAX_WAITING_CONNECTIONS);
		}
		catch (IOException e){
			logger.log(Level.SEVERE, e.getMessage(), e);
			return;
		}
		
		server.setExecutor(null); // user the default executor
		server.createContext("/server", myHttpHandler);
		
		//The other contexts as well
		
		logger.info("Starting HTTP Server.");
		server.start();

	}
	
	/** The handlers */
	private HttpHandler myHttpHandler = new MyHttpHandler();

	
	/**
	 * The main method.
	 *
	 * @param args the arguments
	 * @throws ServerException the server exception
	 */
	public static void main(String[] args) throws ServerException {
		try{
			new Server(3040).run();
		}
		catch (NumberFormatException e)
		{
			throw new ServerException("Not a valid port number.");
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}