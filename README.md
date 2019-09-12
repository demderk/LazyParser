# LazyParser
Simple console command parser. Nothing extra.   

## How to use
 - Download [last release](https://github.com/demderk/LazyParser/releases).
 - Connect this lib.
 - ```C#
    string cin = Console.ReadLine();
    LazyParser.Command inputCommand = new Command(cin);
   ```
 - Done.

## Few thing about it
Class have 3 definitions. Command name, Argument, Parameter.   
Let's look at an example ```git commit -aS -m "Simple example"```.  

| ?                   | Description                 | Comment                                    |
| :---:               | :----------------------:    | :----------------------------------------: |
| git                 |  Command name               | Can be only one.                           | 
| commit              |  Argument                   | Simple argument.                           | 
| -aS                 |  Parameter without addition | ParamData.Data empty.                      |
| -m "Simple example" |  Parameter with addition    | "Simple example" is an addition.           |

### About parameters.
Class ParamData is a description of the parameter.   
Parameters is an argument have "-" at first. Parameters can have additions. All additions are situated in "Data" and split by space(" "). Parameters must always be at the end, overwise everything after the parameter will be additions, except for other parameters. DataString contains additions without splitting.


## Let's try it out!
You can check how it work. There's ready-to-use program in **LazyParser/ConsoleTests** directory.
