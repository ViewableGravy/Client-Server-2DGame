class Sender implements Runnable {
   ArrayList<Character> newInputs;
   ArrayList<Integer> availablePacketID = new ArrayList<Integer>();
   ArrayList<Packet> sentPackets = new ArrayList<Packet>();

  Sender(ArrayList<Character> newInputs) {
    this.newInputs = newInputs;
    for (int i = 1; i <= 1000; ++i) {
      availablePacketID.add(i);
    }
  }

  void run() {
    while (true) {
      try {

        //generate packet from current inputs
        Packet currentPacket = new Packet(availablePacketID.get(0), newInputs);

        availablePacketID.remove(0);

        //generate packet to send to server including previous packets
        String request = GenerateRequest(currentPacket, sentPackets);

        println(request);
        //send to server
        ServerUpdate(request);

        //add the current input packet to sent
        sentPackets.add(currentPacket);

        //wait before trying again
        Thread.sleep(1000);
      } 
      catch(Exception e) {
        println(e);
      }
    }
  }
}


public static String GenerateRequest(Packet currentPacket, ArrayList<Packet> sentPackets) {

  sentPackets.add(currentPacket);
  
  JSONObject credentials = new JSONObject() {
    {
      setString("username", username);
      setInt("sessionToken", SessionToken);
    }
  };  

  JSONArray requests = new JSONArray();
  for (int j = 0; j < sentPackets.size(); j++) {
    Packet pkt = sentPackets.get(j);

    JSONObject internalPacket = new JSONObject();
    internalPacket.setInt("ID", pkt.ID);

    JSONArray keys = new JSONArray();
    for (int i = 0; i < pkt.keyPresses.size(); i++) {
      JSONObject KEY = new JSONObject();
      KEY.setString("value", String.valueOf(pkt.keyPresses.get(i)));
      keys.setJSONObject(i, KEY);
    }
    internalPacket.setJSONArray("keypresses", keys);
    requests.setJSONObject(j, internalPacket);
  }

  JSONObject packet = new JSONObject();
  packet.setJSONObject("credentials", credentials);
  packet.setJSONArray("requests", requests);

  return packet.toString();
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
