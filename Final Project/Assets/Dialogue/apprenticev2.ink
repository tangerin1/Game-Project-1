-> start

=== start ===
You: Placeholder
Will: Placeholder
You: Placeholder
Will: Placeholder
-> main_choices

=== main_choices ===
* [Ask about Dominic] -> ask_dominic
* [Ask about the theater] -> ask_theater
* [Ask about where Will was] -> ask_alibi

=== ask_dominic ===
Will: *response to ask dominic*
-> dominic_choices

=== dominic_choices ===
* [Ask about Dominic's attitude] -> dominic_attitude
* [Ask about Dominic's enemies] -> dominic_enemies
* [Ask about Dominic's last rehearsal] -> dominic_rehearsal

=== dominic_attitude ===
Will: *response to ask about dominics attitude*
-> dominic_followup

=== dominic_enemies ===
Will: *response to ask about dominics enemies*
-> dominic_followup

=== dominic_rehearsal ===
Will: *response to ask about dominics last rehearsal*
-> dominic_followup

=== dominic_followup ===
* [Press about his temper] -> dominic_temper
* [Ask who was nearby] -> dominic_nearby
* [Ask what happened next] -> dominic_next

=== dominic_temper ===
Will: *response to press about his temper*
-> END

=== dominic_nearby ===
Will: *response to ask who was nearby*
-> END

=== dominic_next ===
Will: *response to ask what happened next*
-> END

=== ask_theater ===
Will: *response to ask about the theater*
-> theater_choices

=== theater_choices ===
* [Ask about the missing props] -> theater_props
* [Ask about the power outage] -> theater_power
* [Ask about the backstage mood] -> theater_mood

=== theater_props ===
Will: *response to ask about the missing props*
-> theater_followup

=== theater_power ===
Will: *response to ask about the power outage*
-> theater_followup

=== theater_mood ===
Will: *response to ask about the backstage mood*
-> theater_followup

=== theater_followup ===
* [Ask who was most nervous] -> theater_nervous
* [Ask what changed that night] -> theater_changed
* [Ask what Will noticed] -> theater_noticed

=== theater_nervous ===
Will: *response to ask who was most nervous*
-> END

=== theater_changed ===
Will: *response to ask what changed that night*
-> END

=== theater_noticed ===
Will: *response to ask what will noticed*
-> END

=== ask_alibi ===
Will: *response to ask about where will was*
-> alibi_choices

=== alibi_choices ===
* [Ask where Will went] -> alibi_where
* [Ask who saw him] -> alibi_witness
* [Ask why he left] -> alibi_why

=== alibi_where ===
Will: *response to ask where will went*
-> alibi_followup

=== alibi_witness ===
Will: *response to ask who saw him*
-> alibi_followup

=== alibi_why ===
Will: *response to ask why he left*
-> alibi_followup

=== alibi_followup ===
* [Ask for the exact timing] -> alibi_timing
* [Ask what he heard] -> alibi_heard
* [Ask what he did after] -> alibi_after

=== alibi_timing ===
Will: *response to ask for the exact timing*
-> END

=== alibi_heard ===
Will: *response to ask what he heard*
-> END

=== alibi_after ===
Will: *response to ask what he did after*
-> END
