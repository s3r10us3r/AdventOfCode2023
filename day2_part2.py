from math import inf as infinity

def find_maximum_values(game_str):
    game_str = game_str.split(':')[1]
    cube_sets = game_str.strip().split(';')
    cubes_and_colors = []
    
    for cube_set in cube_sets:
        cubes_and_colors.extend(cube_set.split(','))
    
    cube_dict = {'red' : 0, 'green' : 0, 'blue' : 0}
    for cubes_and_color in cubes_and_colors:
        cubes, color = tuple(cubes_and_color.strip().split(' '))
        cube_num = int(cubes)
        if cube_num > cube_dict[color]:
            cube_dict[color] = cube_num
    return cube_dict

result = 0
for i in range(100):
    max_val_dict = find_maximum_values(input())
    result += max_val_dict['red'] * max_val_dict['blue'] * max_val_dict['green']

print(result)