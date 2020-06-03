import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;

Thread sendingThread = new Thread();
Thread recievingThread = new Thread();
ArrayList<Character> inputs = new ArrayList<Character>();
ArrayList<String> serverMessages = new ArrayList<String>();
//temp credentials
static String username = "ViewableGravy";
static String SessionToken = "7c9e6679-7425-40de-944b-e07fc1f90ae7";

void setup() {

  size(200, 200);
  sendingThread = new Thread(new Sender(inputs));
  sendingThread.start();
  recievingThread = new Thread(new Reciever(serverMessages));
  recievingThread.start();
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
