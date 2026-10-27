using LyserødeSpejdere.Lib;

namespace LyserødeSpejdere.Data;

public interface ITilmeldingRepo
{
    string? Tilmeld(int brugerId, int aktivitetId);
    void Frameld(int brugerId, int aktivitetId);
    List<Aktivitet> HentMineAktiviteter(int brugerId);
    List<Bruger> HentDeltagere(int aktivitetId);
    int HentDeltagerAntal(int aktivitetId);
    string? TjekRegler(int brugerId, int aktivitetId);
    bool ErTilmeldt(int brugerId, int aktivitetId);
}
