using System;

public abstract class Instruction {
  public Instruction(Func<Robot, string, bool> predicate, string param) {
    this.param = param;
    this.predicate = predicate;
  }
  protected string param;
  protected Func<Robot, string, bool> predicate { get; set; }
  public abstract void Run(Robot robot);
}

public interface PredicateFn {
  bool Check(Robot robot);
}

public class PrintInstruction : Instruction {
  public PrintInstruction(Func<Robot, string, bool> predicate, string param) : base(predicate, param)
  {
  }

  public override void Run(Robot robot) {
    if (predicate.Invoke(robot, param)) {
      Console.WriteLine("Print X");
    }
  }
}

public class MoveLeftInstruction : Instruction {
  public MoveLeftInstruction(Func<Robot, string, bool> predicate, string param) : base(predicate, param)
  {
  }

  public override void Run(Robot robot) {
    if (predicate.Invoke(robot, param)) {
      robot.MoveEast();
    }
  }
}