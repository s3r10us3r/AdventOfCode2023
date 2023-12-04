matches = []
count = []

for i in range(220):
    winning_nums, my_nums = input().split(':')[1].split('|')
    wins = [win for win in winning_nums.split(' ') if win != '']
    my = [my for my in my_nums.split(' ') if my != '']
    count.append(1)
    matches.append(0)
    for my in my:
        if my in wins:
            matches[i]+=1

for i, match in enumerate(matches):
    for j in range(i+1, i+1+match):
        count[j] += count[i]

print(sum(count))