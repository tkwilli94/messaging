

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.util.List;
import java.util.Map.Entry;
import java.util.Set;

import com.sun.net.httpserver.HttpExchange;
import com.sun.net.httpserver.HttpHandler;

public class MyHttpHandler implements HttpHandler {

	@SuppressWarnings("restriction")
	@Override
	public void handle(HttpExchange exchange) throws IOException {
		StringBuilder sb = new StringBuilder();
		sb.append("Headers:\n");
		Set<Entry<String,List<String>>> entries = exchange.getRequestHeaders().entrySet();
		for (Entry<String,List<String>> e : entries) {
			sb.append("Key: " + e + '\n');
			for (String s : e.getValue()) {
				sb.append("Value: " + s + '\n');
			}
			sb.append('\n');
		}
		
/*		String queryString = exchange.getRequestURI().getQuery().toString();
		String[] queries = queryString.split("&");
		sb.append("\nQuery String Parameters:\n");
		for (String s : queries) {
			sb.append(s + '\n');
		}*/
		
		sb.append("\n\nBody:\n");	
		BufferedReader br = new BufferedReader(new InputStreamReader(exchange.getRequestBody(),"utf-8"));
		int b;
		while ((b = br.read()) != -1) {
		    sb.append((char) b);
		}
		sb.append('\n');
		br.close();
		System.out.print(sb.toString());
		
		exchange.getResponseHeaders().set("Content-Type", "text/plain");
		//String response = "{\"Okay\", \"Thing\"}";
		exchange.sendResponseHeaders(HttpURLConnection.HTTP_OK, sb.length());
		exchange.getResponseBody().write(sb.toString().getBytes());
		exchange.close();
	}

}
