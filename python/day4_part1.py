result = 0

for i in range(220):
    matches = 0
    winning_nums, my_nums = input().split(':')[1].split('|')
    wins = [win for win in winning_nums.split(' ') if win != '']
    my = [my for my in my_nums.split(' ') if my != '']
    for my_num in my:
        if my_num in wins:
            matches += 1
    if matches != 0:
        result += 2**(matches-1)
print(result)