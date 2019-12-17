import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.util.Scanner;

Thread sendingThread = new Thread();
ArrayList<Character> inputs = new ArrayList<Character>();

void setup() {
  
  size(200,200);
  sendingThread = new Thread(new Sender(inputs));
  sendingThread.start();
 
  
}

void draw() {
  
}

void mousePressed() {
  
}


class Sender implements Runnable {
  ArrayList<Character> keyPresses;

  Sender(ArrayList<Character> keyPresses) {
    this.keyPresses = keyPresses;
  }

  void run() {
    while (true) {
      try {
        String request = "test";        
        for (Character chr : keyPresses) {
          //generate string to send
        }

        ServerUpdate(request);

        Thread.sleep(20);
      } 
      catch(Exception e) {
        println(e);
      }
    }
  }
}

public static void ServerUpdate(String request) throws IOException {
  int PORT = 1234;
  InetAddress DESTINATION_IP = InetAddress.getLocalHost(); 
  DatagramSocket socket = new DatagramSocket(); 
  byte buf[] = null; 
  
  buf = request.getBytes(); //convert string to bytes
  socket.send(new DatagramPacket(buf, buf.length, DESTINATION_IP, PORT)); //send to server

  socket.close();
}
