# Interval-Alpha-Beta-and-Alpha-beta-comparing

This project created for my school research to find a optimal algorithm for Dynamic Difficulty Adjusting (DDA) AI algorithm in unsolved abstract strategy games.
DDA AI adjusts its difficulty to player level so that player can be challanged since they are almost playing with themselves.

In this project I created an Algorithm called Interval Alpha-Beta Algorithm which is modified version of Alpha-Beta algoritm.

Purpose of Interval AB algorithm is, choosing best node in interval with alpha-beta cuts in board games. Therefore, difficulty of AI can be adjusted dynamically.

Interval Alpha-beta has 2 Conditions;

Condition 1 : if (alpha≥beta) alpha-beta cut;
 
Condition 2 : if (alpha>y || alpha<x) skip that node;

For example, if interval is 3-7, it will try to choose node with value of 7 not 2 or 10. However, since it works like normal alpha-beta with min-max, it might choose different value in that interval.

As you can see on picture, if there is a tree like this,
![image](https://user-images.githubusercontent.com/62457417/200162742-5f6af6f8-3171-44e6-be36-cf4dbaa9144c.png)

result will be like this

![image](https://user-images.githubusercontent.com/62457417/200162825-7f678314-2f77-4856-b2e9-b0d4f09eca18.png)

Comparison of Interval AB and AB algorithms in a tree with nodes of 4 children each

![image](https://user-images.githubusercontent.com/62457417/200162870-a5726d87-2de3-4b80-9b57-d81d052ea891.png)

![image](https://user-images.githubusercontent.com/62457417/200163097-ac5515ff-f7a6-488a-97fb-03c47a91b5e2.png)
