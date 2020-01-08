import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.util.Scanner;

Thread sendingThread = new Thread();
ArrayList<Character> inputs = new ArrayList<Character>();
//temp credentials
static String username = "ViewableGravy";
static int SessionToken = 123456;

void setup() {

  size(200, 200);
  sendingThread = new Thread(new Sender(inputs));
  sendingThread.start();
}

void draw() {
  //println(inputs);
}

void keyPressed() {
  if (!inputs.contains(key))
    inputs.add(key);
}

void keyReleased() {
  if (inputs.contains(key))
    inputs.remove((Character)key);
}
