using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;


public class CCompiler
{
	public CCompiler()
	{

	}

    /**
     * Main function that we will use to compile the code;
     * !!! TEMP: Currently compileErrors can contain some errros that can occur inside the function from dll; This is active only for debug
     */
    public static bool Compile( string sourceCode, out string compileErrors )
    {
        compileErrors = "";

        // we need the path to the solution that we use to compile 
        // current path is were the web site files are found not the server
        // if you use GetCurrentDirectory will not work
        string currPath = HttpRuntime.AppDomainAppPath;
        string solutionPath = currPath + "CompilerDummy\\";

        string compileRezult = _Compile(sourceCode, solutionPath);

        if (compileRezult != null )
        {
            compileErrors = compileRezult;
            return false;
        }

        return true;
    }

    //------------------------------------------------------------------------------------------------------------------------------
    /** Native function used to compile; found in TehniciCompilare.dll with the name Compile
    // C function prototype: bool _stdcall Compile ( const char* sourceCode, const char* solutionPath );
     */
    //[DllImportAttribute(@"e:\Programming\Workspace GameDev\Workspace Licenta\Game\Release\TehniciCompilare.dll", EntryPoint = "_Compile", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [DllImportAttribute(@"TehniciCompilare.dll", EntryPoint = "_Compile", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string _Compile(
                                        [MarshalAs(UnmanagedType.LPStr)]
                                        string cppFileName,                 // path to solution to compile
                                        [MarshalAs(UnmanagedType.LPStr)]
                                        string dummySolutionPath            // path to dummy solution used to compile
                                    );

    //------------------------------------------------------------------------------------------------------------------------------

    [System.Runtime.InteropServices.DllImportAttribute(@"e:\Programming\Workspace GameDev\Workspace Licenta\Game\Release\TehniciCompilare.dll", EntryPoint = "GetTestNumber", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    private static extern int GetTestNumber();

    //-------------------------------------------------------------------------------------------------------------------------------

    [DllImportAttribute(@"e:\Programming\Workspace GameDev\Workspace Licenta\Game\Release\TehniciCompilare.dll", EntryPoint = "GetTestString", CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
    [return: MarshalAs(UnmanagedType.LPStr)]
    private static extern string GetTestString ( );

    //-------------------------------------------------------------------------------------------------------------------------------


}//class Compiler