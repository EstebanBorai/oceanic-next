import pickle
from sanitize import sanitize
from athlete_list import AtheleteList

# this is a comment in python
pickle_filename = 'athletes.pickle'

def get_coach_data(filename):
	try:
		with open(filename) as f:
			data = f.readline()
			data = data.strip().split(',')
			dic = {
				'Name': data.pop(0),
				'Birthdate': data.pop(0),
				'Best Times': str(sorted(set([sanitize(t) for t in data]))[0:3])
			}
		return dic
	except IOError as ioerr:
		print('File Error! ' + str(ioerr))

def put_to_store(files_list):
	all_athletes = {}
	for file in files_list:
		ath = get_coach_data(file)
		all_athletes[ath['Name']] = ath
	try:
		with open(pickle_filename, 'wb') as ath_file:
			pickle.dump(all_athletes, ath_file)
	except IOError as err:
		print('File Error! ' + str(err))
	return all_athletes

def get_from_store():
	all_athletes = {}
	try:
		with open(pickle_filename, 'rb') as ath_file:
			all_athletes = pickle.load(ath_file)
	except IOError as err:
		print('File Error! ' + str(err))
	return all_athletes
