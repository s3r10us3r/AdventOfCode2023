from string import digits

schematic = []
star_dict = dict()

def check_if_star(row, col, num):
    if schematic[row][col] == '*':
        if (row, col) in star_dict:
            star_dict[(row, col)].append(num)
        else:
            star_dict[(row,col)] = [num]

for i in range(10):
    schematic.append('.' + input() + '.')

line_len = len(schematic[0]) - 1

schematic.insert(0, '.' * (line_len + 2))
schematic.append('.' * (line_len + 2))

result = 0

for j, line in enumerate(schematic[1:-1]):
    i = 0
    j += 1
    while i < line_len:
        if line[i] in digits:
            num = ''
            start = i
            finish = 0
            while line[i] in digits and i < line_len:
                num += line[i]
                finish = i
                i += 1
            
            for s in range(start - 1, finish+2):
                check_if_star(j-1, s, num)
                check_if_star(j+1, s, num)

            check_if_star(j, start-1, num)
            check_if_star(j, finish+1, num)
        else:
            i+=1

for ls in star_dict.values():
    if len(ls) == 2:
        result += int(ls[0]) * int(ls[1])

print(result)