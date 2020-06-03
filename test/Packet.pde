public class Packet {
  public int ID;
  ArrayList<Character> keyPresses;

  Packet(int ID, ArrayList<Character> keyPresses) {
    this.ID = ID;
    this.keyPresses = new ArrayList<Character>(keyPresses);
  }
}
