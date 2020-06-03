ArrayList<Packet> sentPackets = new ArrayList<Packet>();

class Sender implements Runnable {
  ArrayList<Character> newInputs;
  int availablePacketID;

  Sender(ArrayList<Character> newInputs) {
    this.newInputs = newInputs;
    availablePacketID = 1;
  }

  void run() {
    while (true) {
      try {

        //generate packet from current inputs
        Packet currentPacket = new Packet(availablePacketID++, newInputs);

        //generate packet to send to server including previous packets
        String request = GenerateRequest(currentPacket, sentPackets);

        //println(request);
        //send to server
        ServerUpdate(request);

        //add the current input packet to sent
        sentPackets.add(currentPacket);

        //wait before trying again
        Thread.sleep(10);
      } 
      catch(Exception e) {
        println(e);
      }
    }
  }
}


public static String GenerateRequest(Packet currentPacket, ArrayList<Packet> sentPackets) {

  //maybe store these locally as JSONObjects since they probably don't need to be used for anything else
  JSONObject credentials = new JSONObject() {
    {
      setString("username", username);
      setString("sessionToken", SessionToken);
    }
  };  

  JSONArray requests = new JSONArray();
  for (int j = 0; j < sentPackets.size(); j++) {
    requests.setJSONObject(j, ConstructRequest(sentPackets.get(j)));
  }
  requests.setJSONObject(sentPackets.size(), ConstructRequest(currentPacket));

  JSONObject packet = new JSONObject();
  packet.setJSONObject("credentials", credentials);
  packet.setJSONArray("requests", requests);

  return packet.toString();
}

public static JSONObject ConstructRequest(Packet pkt) {
  JSONObject internalPacket = new JSONObject();
  internalPacket.setInt("ID", pkt.ID);

  JSONArray keys = new JSONArray();
  for (int i = 0; i < pkt.keyPresses.size(); i++) {
    JSONObject KEY = new JSONObject();
    KEY.setString("value", String.valueOf(pkt.keyPresses.get(i)));
    keys.setJSONObject(i, KEY);
  }
  internalPacket.setJSONArray("keypresses", keys);
  return internalPacket;
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

public class Reciever implements Runnable {
  ArrayList<String> serverMessage;
  public Reciever(ArrayList<String> serverMessage) {
    this.serverMessage = serverMessage;
  }

  public void run() {
    DatagramSocket datagramSocket;
    try {
      datagramSocket = new DatagramSocket(1011);

      //probably enough bytes for now
      byte[] buffer = new byte[30000];
      DatagramPacket packet = new DatagramPacket(buffer, buffer.length);

      while (true) {
        println("ready to recieve");
        datagramSocket.receive(packet);
        println(new String(buffer));
        serverMessage.add(new String(buffer));
        ApplyServerMessage();
      }
    } 
    catch(Exception e) {
      e.printStackTrace();
    }
  }
  
  private void ApplyServerMessage() {
    for(String str : serverMessage) { //<>//
      JSONObject json = JSONObject.parse(str);
      int requestNumber = Integer.parseInt(json.get("mostRecentRequest").toString());
      for(int i = sentPackets.size() - 1; i >= 0; --i) {
        if(sentPackets.get(i).ID <= requestNumber)
          sentPackets.remove(sentPackets.get(i));
      }
      println(requestNumber);
      println(sentPackets.size());
    }
    serverMessage.clear();
  }
  
  
}
