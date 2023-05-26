/** @format */
// Simple table component:
import { TripHttpClient } from './tripservice/client/trip.http-client';
import { Excursie } from './tripservice/models/Excursie';
import { useState } from 'react';

interface UserExcursie {
	id: string;
	nume_obiectiv: string;
	nume_firma: string;
	ora: number;
	pret: number;
	nr_locuri_totale: number;
}

const INITIAL_TRIPS: UserExcursie[] = [
	{
		id: '1',
		nume_obiectiv: 'nume_obiectiv',
		nume_firma: 'nume_firma_transport',
		ora: 1,
		pret: 2,
		nr_locuri_totale: 500,
	},
];

// THE CODE FOR THE PAGE::::

export const TablePage = () => {
	const [trips, setTrips] = useState(INITIAL_TRIPS);

	const renderHeader = () => {
		const capitalize = (word: string) => {
			return word[0].toUpperCase() + word.slice(1);
		};

		return (
			<tr>
				{Object.keys(INITIAL_TRIPS[0]).map((key) => (
					<th>{capitalize(key)}</th>
				))}
			</tr>
		);
	};

	const renderTrips = () => {
		return trips.map(
			({
				id,
				nume_obiectiv: nume_obiectiv,
				nume_firma: nume_firma,
				ora,
				pret,
				nr_locuri_totale: nrLocuriTotale,
			}) => {
				return (
					<tr key={id}>
						<td style={{ padding: '10px', border: '1px solid black' }}>
							{id}
						</td>
						<td style={{ padding: '10px', border: '1px solid black' }}>
							{nume_obiectiv}
						</td>
						<td style={{ padding: '10px', border: '1px solid black' }}>
							{nume_firma}
						</td>
						<td style={{ padding: '10px', border: '1px solid black' }}>
							{ora}
						</td>
						<td style={{ padding: '10px', border: '1px solid black' }}>
							{pret}
						</td>
						<td style={{ padding: '10px', border: '1px solid black' }}>
							{nrLocuriTotale}
						</td>
					</tr>
				);
			},
		);
	};

	async function convertTripToVisibleTrip(
		trip: Excursie,
	): Promise<UserExcursie> {
		const firmaTransport = await TripHttpClient.getFirmaById(
			trip.id_firma_transport,
		);
		const obiectiv = await TripHttpClient.getObiectivById(
			trip.id_obiectiv,
		);
		return {
			id: trip.id,
			nume_obiectiv: obiectiv.nume,
			nume_firma: firmaTransport.nume,
			ora: trip.ora,
			pret: trip.pret,
			nr_locuri_totale: trip.nr_locuri_totale,
		};
	}

	const loadTrips = async () => {
		const trips = await TripHttpClient.getTrips();
		const visibleTrips: UserExcursie[] = await Promise.all(
			trips.map(
				async (trip): Promise<UserExcursie> =>
					await convertTripToVisibleTrip(trip),
			),
		);

		setTrips(visibleTrips);
	};

	const addTrip = async () => {
		const nume_obiectiv = (
			document.getElementById('nume_obiectiv') as HTMLInputElement
		).value;
		const nume_firma = (
			document.getElementById('nume_firma') as HTMLInputElement
		).value;
		const ora = parseInt(
			(document.getElementById('ora') as HTMLInputElement).value,
		);
		const pret = parseFloat(
			(document.getElementById('pret') as HTMLInputElement).value,
		);
		const nr_locuri = parseInt(
			(document.getElementById('nr_locuri') as HTMLInputElement).value,
		);

		await TripHttpClient.createTrip(
			nume_obiectiv,
			nume_firma,
			ora,
			pret,
			nr_locuri,
		);
	};

	const updateTrip = async () => {
		const id = (document.getElementById('idUpdate') as HTMLInputElement)
			.value;
		const nume_obiectiv = (
			document.getElementById('nume_obiectivUpdate') as HTMLInputElement
		).value;
		const nume_firma = (
			document.getElementById('nume_firmaUpdate') as HTMLInputElement
		).value;
		const ora = parseInt(
			(document.getElementById('oraUpdate') as HTMLInputElement).value,
		);
		const pret = parseFloat(
			(document.getElementById('pretUpdate') as HTMLInputElement).value,
		);
		const nr_locuri = parseInt(
			(document.getElementById('nr_locuriUpdate') as HTMLInputElement)
				.value,
		);
		await TripHttpClient.updateTrip(
			id,
			nume_obiectiv,
			nume_firma,
			ora,
			pret,
			nr_locuri,
		);
	};

	return (
		<>
			<table>
				{renderHeader()}
				<tbody>{renderTrips()}</tbody>
			</table>
			<button onClick={loadTrips}>Load trips</button>
			<br></br>
			ADD TRIP:
			<label htmlFor='nume_obiectiv'>nume_obiectiv</label>
			<input type='text' id='nume_obiectiv' name='nume_obiectiv' />
			<label htmlFor='nume_firma'>nume_firma</label>
			<input type='text' id='nume_firma' name='nume_firma' />
			<label htmlFor='ora'>ora</label>
			<input type='text' id='ora' name='ora' />
			<label htmlFor='pret'>pret</label>
			<input type='text' id='pret' name='pret' />
			<label htmlFor='nr_locuri'>nr_locuri</label>
			<input type='text' id='nr_locuri' name='nr_locuri' />
			<button
				onClick={async () => {
					await addTrip();
				}}>
				Add trip
			</button>
			<br></br>
			UPDATE TRIP:
			<label htmlFor='idUpdate'>id</label>
			<input type='text' id='idUpdate' name='idUpdate' />
			<label htmlFor='nume_obiectivUpdate'>nume_obiectiv</label>
			<input
				type='text'
				id='nume_obiectivUpdate'
				name='nume_obiectivUpdate'
			/>
			<label htmlFor='nume_firmaUpdate'>nume_firma</label>
			<input type='text' id='nume_firmaUpdate' name='nume_firmaUpdate' />
			<label htmlFor='oraUpdate'>ora</label>
			<input type='text' id='oraUpdate' name='oraUpdate' />
			<label htmlFor='pretUpdate'>pret</label>
			<input type='text' id='pretUpdate' name='pretUpdate' />
			<label htmlFor='nr_locuriUpdate'>nr_locuri</label>
			<input type='text' id='nr_locuriUpdate' name='nr_locuriUpdate' />
			<button
				onClick={async () => {
					await updateTrip();
				}}>
				Update trip
			</button>
			<br></br>
			DELETE TRIP:
			<label htmlFor='idDelete'>id</label>
			<input type='text' id='idDelete' name='idDelete' />
			<button
				onClick={async () => {
					const id = (
						document.getElementById('idDelete') as HTMLInputElement
					).value;
					await TripHttpClient.deleteTrip(id);
				}}>
				Delete trip
			</button>
		</>
	);
};
