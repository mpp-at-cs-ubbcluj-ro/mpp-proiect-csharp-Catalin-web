/** @format */

import axios from 'axios';
import {
	Excursie,
	FirmaTransport,
	ObiectivTuristic,
} from '../models/Excursie';

export class TripHttpClient {
	private static readonly baseurl: string =
		'http://localhost:12500/v1';

	public static async createTrip(
		NumeObiectiv: string,
		NumeFirma: string,
		Ora: number,
		Pret: number,
		NrLocuriTotale: number,
	) {
		await axios.post(
			`${this.baseurl}/exursie/add`,
			{},
			{
				params: {
					numeObiectiv: NumeObiectiv,
					numeFirma: NumeFirma,
					ora: Ora.toString(),
					pret: Pret.toString(),
					nrLocuriTotale: NrLocuriTotale.toString(),
				},
			},
		);
	}

	public static async getTrips(): Promise<Excursie[]> {
		return (await axios.post(`${this.baseurl}/query/trips`)).data;
	}

	public static async deleteTrip(id: string) {
		return await axios.post(
			`${this.baseurl}/exursie/delete`,
			{},
			{
				params: { idExcursie: id },
			},
		);
	}

	public static async updateTrip(
		id: string,
		NumeObiectiv: string,
		NumeFirma: string,
		Ora: number,
		Pret: number,
		NrLocuriTotale: number,
	) {
		return await axios.post(
			`${this.baseurl}/exursie/update`,
			{},
			{
				params: {
					idExcursie: id,
					numeObiectiv: NumeObiectiv,
					numeFirma: NumeFirma,
					ora: Ora,
					pret: Pret,
					nrLocuriTotale: NrLocuriTotale,
				},
			},
		);
	}

	public static async getObiectivById(
		IdObiectiv: string,
	): Promise<ObiectivTuristic> {
		return (
			await axios.post(
				`${this.baseurl}/query/obiectiv/id`,
				{},
				{
					params: { IdObiectiv: IdObiectiv },
				},
			)
		).data;
	}

	public static async getFirmaById(
		idFirma: string,
	): Promise<FirmaTransport> {
		return (
			await axios.post(
				`${this.baseurl}/query/firma/id`,
				{},
				{
					params: { idFirma: idFirma },
				},
			)
		).data;
	}
}
