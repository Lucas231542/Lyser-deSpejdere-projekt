using LyserødeSpejdere.Lib;

namespace LyserødeSpejdere.Data;

public interface IAktivitetTypeRepo
{
    List<AktivitetType> GetAll();
    AktivitetType? GetById(int aktivitetTypeId);
    List<Faerdighed> HentKrav(int aktivitetTypeId);
}
