INCLUDE globals.ink

{ Boyd_Calls == 0: -> main}
{ Boyd_Calls == 1: -> secondcall}
{ Boyd_Calls == 2: -> thirdcall}
{ Boyd_Calls == 3: -> forthcall}
-> main

== main ==
Congratulations on breaking my <i>super secret</i> code. #textcolor:blue
I'd love to reward you with some juicy government intel... #textcolor:green
But unfortunately I have to attend to my steamed hams. #textcolor:red
~ Boyd_Calls = 1 
 + [sorry to bother you]
    -> chosen
 + [hey what's with the attitude]
    -> chosen
-> DONE

== secondcall ==
You again?
Didn't you hear me the first time?
I'm cooking dinner for Superintendent Chalmers!
Call back tomorrow, why don't ya?
~ Boyd_Calls = 2
-> DONE

== thirdcall ==
Okay, this is obviously important to you.
I'll tell ya what.
If you can solve this puzzle, I'll listen to what you have to say.
~ isPuzzleDisplayON = true
~ Boyd_Calls = 3
-> DONE

== forthcall ==
The number you have dialed is not in service.
-> DONE

=== chosen ===

Hey I said leave me alone!
 + [Okay, okay]
 -> DONE
 + [What if I don't want to?]
-> END