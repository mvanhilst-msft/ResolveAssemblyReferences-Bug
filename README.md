Visual Studio demonstrating what appears to be a bug in MSBuild's ResolveAssemblyReferences, specifically around binding redirect generation.

# Setup

     Project C (Console App)
      |
      +-[ProjectReference]-> Project A (Class Library)
      |                       |
      |                       +-[PackageReference]-> Microsoft.Diagnostics.Tracing.EventSource 1.1.28
      |                       |                       |
      |                       |                       +-[dependency]-> Microsoft.Diagnostics.Tracing.EventSource.Redist 1.1.28
      |                       |                                                 /|\
      |                       |                                             [comes from]
      |                       |                                                  |
      |                       +-[code reference]-> Microsoft.Diagnostics.Tracing.EventSource.dll 1.1.28.0
      |
      +-[ProjectReference]-> Project B (Class Library)
                              |
                              +-[PackageReference]-> Microsoft.Diagnostics.Tracing.EventSource.Redist 2.2.0
                              |                                         /|\
                              |                                     [comes from]
                              |                                          |
                              +-[code reference]-> Microsoft.Diagnostics.Tracing.EventSource.dll 2.0.3.0

Project A and B each have a simple Hello World example program using Microsoft.Diagnostics.Tracing.EventSource. Project C invokes the code from A and B.

# Expected Behavior

No issues.

# Actual Behavior

    Unhandled Exception: System.IO.FileLoadException: Could not load file or assembly 'Microsoft.Diagnostics.Tracing.EventSource, Version=1.1.28.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)
       at ProjectA.ClassA.DoThing()
       at ProjectC.Program.Main(String[] args) in C:\repos\misc\ResolveAssemblyReferences-Bug\ResolveAssemblyReferencesBug\ProjectC\Program.cs:line 10

