# Overview

This is a proof of concept. The purpose of this PoC is to determine how the logging output of an application can be captured (or intercepted) in order to determine whether a program's run (so far), has been successful.

# Acceptance criteria

- The functionality can be leveraged without introducing breaking changes to an existing codebase (i.e. pluggable)
- Any logging with the [`LogEventLevel`](http://sourcebrowser.io/Browse/serilog/serilog/src/Serilog/Events/LogEventLevel.cs#20) of `Fatal` and `Error` are registered as system failures. 
- Functionality is provided to consume the results of the process run without knowing the intricacies of how they are collected

# Dependencies

- [Serilog](https://github.com/serilog)

Serilog is the Logging framework of choice in this PoC. Serilog's logging output is sent to classes called _sinks_, which can be used to forward the logging to whatever destination. In this example we use the below sinks.

- [Serilog Observable sink](https://github.com/serilog/serilog-sinks-observable)

The Observable sink is a sink which offers the ability to forward logging input to other classes (the Observers). We will leverage this to forward input to our custom made classes.

- [Serilog Colored Console sink](https://github.com/serilog/serilog-sinks-coloredconsole)

The Colored Console sink is a sink which forwards all logging to the console, with pretty colors. This is merely used for having visual feedback when running the application.
