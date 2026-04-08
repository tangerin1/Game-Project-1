
Understudy conversation.

 * Talk to the understudy 
 -> understudy
    
===understudy
- You: Hello, Will. What brings you here today?
- Will: I'm here delivering some mail for Dominic. I've been worried because he hasn't been home in a while. ->understudyOptions

===understudyOptions

* [How long has he been away from home?]-> awayFromHome
* [Did something happen at home that is keeping him away?]-> homeWorries
* [What brought you to his house?]-> toHouse
* [Did you notice anything while the power was out?]->understudyPowerOut
* [End Conversation]-> thanksUnderstudy

===understudyPowerOut
- You: Did you notice anything while the power was out?
- Will: I didn't see anything, but I heard a gunshot, a clatter, and footsteps. Did something happen?

* You: That's what I'm trying to figure out. 
    ->understudyOptions
* <i>(if body was found)</i> You: Dominic was murdered. 
    ->understudyDominic

===understudyDominic



->generalOptions

===awayFromHome
- You: How long has he been away from home?
- Will: I don't know for sure. I think that it's been a few weeks. 

-> understudyOptions

===homeWorries
- You: Did something happen at home that is keeping him away?
- Will: They found someone on the property that was sent by his wife. Some sort of private investigator.
- Will: I thought he was staying with his wife to look like a good husband until I saw his subpoena papers
- <i> Will gestures at the papers </i>

->understudyOptions

===toHouse
- You: What brought you to his house?
- <i>Will hesitates for a second...</i>
- Will: Actually, I've been seeing one of the chefs lately. He called me over.
- Will: The environment at the house felt shockingly calm and he told me it was because Dominic wasn't home. 
- Will: I figured that bringing his mail to work would help keep their peace a little longer. 

->understudyOptions

===thanksUnderstudy
- You: Thank you for speaking to me, I don't have any other questions for you at the moment.
-> generalOptions


===generalOptions

->END