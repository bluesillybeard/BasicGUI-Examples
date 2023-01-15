﻿using System.Reflection;
public sealed class Program
{
    public static void Main()
    {
        MethodInfo[] methods = GetRunners();
        System.Console.WriteLine("Please choose an example to run. Options:");
        for(int i=0; i<methods.Length; i++)
        {
            System.Console.WriteLine("\t" + i + ": " + methods[i].DeclaringType.Name);
        }
        bool error = false;
        int index = -1;
        do
        {
            string? text = System.Console.ReadLine();
            if(text is null){System.Console.WriteLine("null line!");error=true;continue;}
            error = !int.TryParse(text, out index);
            if(error){System.Console.WriteLine("Not a number apparently");error=true;continue;}
            if(index < 0){System.Console.WriteLine("You gonna gimme a positive index");error=true;continue;}
            if(index >methods.Length-1){System.Console.WriteLine("Out of bounds");error=true;continue;}
        }while(error);
        methods[index].Invoke(null, null);
    }

    static MethodInfo[] GetRunners()
    {
        List<MethodInfo> outMethods = new List<MethodInfo>();
        var types = System.Reflection.Assembly.GetCallingAssembly().GetTypes();
        //Go through every type in this assembly
        foreach(System.Type type in types)
        {
            //If the type stards with "Example", then it is a possible example class
            if(type.Name.StartsWith("Example") && type.IsClass)
            {
                //go through every method in that type
                var methods = type.GetMethods();
                foreach(var method in methods)
                {
                    //If the method is static, is called "Run", is public, and has zero parameters, then it's what we're looking for.
                    if(method.IsStatic && method.Name == "Run" && method.IsPublic && method.GetParameters().Length == 0)
                    {
                        outMethods.Add(method);
                    }
                }
            }
        }
        return outMethods.ToArray();
    }
}