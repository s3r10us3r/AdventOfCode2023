from string import digits

schematic = []

for i in range(140):
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
            
            adj_symbols = ''

            adj_symbols += schematic[j-1][start - 1 : finish + 2]
            adj_symbols += schematic[j][start-1]
            adj_symbols += schematic[j][finish + 1]
            adj_symbols += schematic[j+1][start - 1 : finish + 2]
            print(f'{num} {adj_symbols}')
            adj_symbols = adj_symbols.replace('.', '')
            if len(adj_symbols) > 0:
                result += int(num)    
        else:
            i+=1

print(result)