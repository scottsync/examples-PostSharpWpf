using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;

[assembly: Log(AttributePriority = 1, AttributeTargetMemberAttributes = MulticastAttributes.Protected | MulticastAttributes.Internal | MulticastAttributes.Public | MulticastAttributes.Private)]



/*
 * Use following attributes to apply logging to certain namespaces.
 * Log(AttributeTargetTypes = "WpfApp1.ViewModel*", ...)
 * 
 * Default, all apply. The "AttributeTargetTypes" could be coupled with "AttributePriority" and "AttributeTargetMemberAttributes" to dig into hotspots.
 */


/*
levels taken from https://doc.postsharp.net/t_postsharp_patterns_diagnostics_loglevel
	None	| 0 | No message should be logged.
	Trace	| 1	| The message should be logged at Trace level (when applicable).
	Debug	| 2 | The message should be logged at Debug level (when applicable).
	Info	| 3 | The message should be logged at Info level (when applicable).
	Warning | 4 | The message should be logged at Warning level (when applicable).
	Error	| 5 | The message should be logged at Error level (when applicable).
	Critical| 6 | The message should be logged at Critical level (when applicable).
 */