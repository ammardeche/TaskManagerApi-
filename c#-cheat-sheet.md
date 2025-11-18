## collection check

if(list?.Any() == true ) ==> this mean the list has an items

<!--  [tsk , task2 .......... ] -->

if(list?.Any() != true ) ==> this mean the list null or empty
if (list == null || !list.Any()) // Explicit check

## common pitFalls

!list.any() crashes if list is null
!list.count() > crashes if list is null
list.First() // Crashes if empty

## remember

NULL = no collection exist
empty = collection exist but empty
Always handle both cases
