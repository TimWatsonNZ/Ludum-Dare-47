using System;

public static class Commander {
  public static Func<Robot, string, bool> Always = (Robot robot, string param) => true;
  public static Func<Robot, string, bool> RobotXLessThan = (Robot robot, string param) => robot.transform.position.x < Int32.Parse(param);

  public static void AddCommand(Robot robot, Command command, string param) {
    switch(command) {
      case Command.Print:
        robot.AddInstruction(new PrintInstruction(Always, ""));
        break;
      case Command.MoveLeft:
        robot.AddInstruction(new MoveLeftInstruction(RobotXLessThan, param));
        break;
    }
  }
}

public enum Command {
  Print,
  MoveLeft
};