Pliki bez komentarza:
//  Potwierdzam samodzielność powyższej pracy oraz niekorzystanie przeze mnie z niedozwolonych źródeł
//  <imię> <nazwisko>
NIE BĘDĄ SPRAWDZANE (trzeba dodać w Program, nie trzeba dodawać nigdzie w Init)


Travel Agencies

Zadanie polega na zaimplementowaniu programu do przeglądania stron internetowych z ofertami z trzech fałszywych biur podróży (Travel Agency). Biura te nie mają rzetelnej oferty, ale bazują na danych z takich serwisów jak Booking.com czy TripAdvisor.com. Dodatkowo wstawiają zdjęcia, które nie mają wiele wspólnego z rzeczywistością, jak i nieprawdziwe opinie/recenzje o ich ofertach.


---BAZY DANYCH---

Każde biuro podróży korzysta z tych samych baz danych. Dane należy czytać w zadanej kolejności. Dostępne bazy danych:
	-BookingDatabase: przechowuje pokoje do wynajęcia podzielone na nieznane nam kategorie. 
	Każda kategoria jest listą jednokierunkową, a każdy jej węzeł przechowuje nazwę pokoju, cenę i ocenę. 
	Należy brać po jednym pokoju po kolei z kolejnych kategorii (najpierw pierwszy pokój z pierwszej kategorii,
	potem pierwszy z drugiej kategorii,..., potem pierwszy z n-tej kategorii, potem drugi z pierwszej kategorii..)
	-OysterDatabase: przechowuje recenzje wycieczek wraz z ich autorami w drzewie, po którym należy przechodzić jakby
	to było drzewo BST (lewe podrzewo -> obecny węzeł -> prawe poddrzewo)
	-ShutterStockDatabase: przechowuje zdjęcia w nieznanym nam podziale na kategorie/grupy/albumy i 
	ich zagnieżdżeń do max 3 poziomów. Każda grupa, podgrupa lub podpodgrupa może być pusta lub nawet być nullem. 
	Przechowywane są całe zestawy metadanych, które mogą się przydać później.
	-TripAdvisorDatabase: przechowuje dane o wycieczkach w dość dziwny sposób. 
	Id wycieczek przechowywane są oddzielne (należy wykorzystywać je po kolei). 
	Ceny, oceny i kraje znajdują się w odpowiednich słownikach. 
	Nazwy wycieczek są podzielone na nieznane kategorie i szukana nazwa (odpowiadająca danemu id) znajduje się
	w jednym ze słowników. Może się okazać, że dane są niekompletne i brakuje dowolnej danej, wtedy 
	należy pominąć daną wycieczkę i od razu przejść od kolejnej.
	
Dane do powyższych baz zostały wygenerowane w metodzie Run.

Wymagania:
	-Każde z biur podróży przechodzi po bazie danych w zadanej kolejności niezależnie, tzn. odczyty z danej bazy danych przez jedno biuro nie mają związku/wpływu z odczytami przez inne biura.
	-Jeśli biuro podróży przejdzie przez wszystkie dane to zaczyna przeglądanie od początku.
	-Nie można przechowywać danych. Wczytywane są pojedynczo tylko jak są potrzebne.
	-Dodatkowym warunkiem jest, aby wszystkie bazy danych wystawiły ten sam interfejs/interfejsy (mogą być różnie parametryzowane).


--SZYFROWANIE---

Dostarczone dane są częściowo zaszyfrowane. Zaszyfrowane są liczby (int), 
jednak wartości int kodowane są jako napisy z zachowaniem wszystkich cyfr aby możliwe było poprawne odkodowanie.
Zakodowane pola są oznaczone komentarzem. Każda baza danych ma inny szyfr, ale studenci MiNI Kreatywnie Rozwiązali Problem 
ich zaszyfrowania i dostarczyli nam wymagane informacje. Wykorzystane szyfrowania:
	-BookingDatabase: FrameCodec(n=2) -> ReverseCodec -> CezarCodec(n=-1) -> SwapCodec
	-ShutterStockDatabase: CezarCodec(n=4) -> FrameCodec(n=1) -> PushCodec(n=-3) -> ReverseCodec
	-TripAdvisorDatabase: PushCodec(n=3) -> FrameCodec(n=2) -> SwapCodec -> PushCodec(n=3)
	-OysterDatabase: Nic (brak danych do zakodowania/rozkodowania)
, gdzie strzałki oznaczują kierunkek kodowania, czyli aby rozkodować należy przeprowadzić operacje odwrotne od końca. 

Każdy kodek działa tylko na cyfrach z nieujemnych liczbach całkowitych (przed użyciem trzeba rozbić liczbę na cyfry). Opisy kodowania(szyfrowania) poszczególnych kodeków:
	-FrameCodec - obkłada ciąg cyfr ramką kolejnych liczb długości n, gdzie 0<=n<=9. Dla n=3 777777->123777777321
	-ReverseCodec - odwraca kolejność cyfr. 123456->654321
	-PushCodec - przesuwa cyfry w prawo o n zawijając ciąg cyfr, gdzie n jest dowolne. Dla n=3 112233445566->566112233445
	-CezarCodec - zwiększa cyfry o n mod10(cyfra musi pozostać cyfrą), gdzie n jest dowolne. Dla n=3 56789->89012
	-SwapCodec - każda kolejna para cyfr jest zamieniana miejscami. Jeśli jest nieparzysta ilość cyfr to ostatnio zostaje bez zmian. 1234567->2143657

Przykłady szyfrowania:
	-BookingDatabase: 5 -> 12521 -> 12521 -> 01410 -> 10140,  199 -> 1219921 -> 1299121 -> 0188010 -> 1088100
	-ShutterStockDatabase: 1080 -> 5424 -> 154241 -> 241154 -> 451142,  1920 -> 5364 -> 153641 -> 641153 -> 351146
	-TripAdvisorDatabase: 75 -> 57 -> 125721 -> 217512 -> 512217,  255 -> 255 -> 1225521 -> 2152251 -> 2512152

Wymagania:
	-Pojedyńczy kodek nie wie czy szyfruje prawdziwe dane czy zaszyfrowane (nie wie czy jest kolejnym elementem łańcucha szyfrowania).
	-Dla sprawdzenia poprawności odszyfrowania rozszyfrowaną liczbę należy zaszyfrować i sprawdzić czy wynik jest identyczny z liczbą wejściową. W razie różnic pominąć błędne dane. Nie jest to metoda dająca dużo informacji, ale ma szansę znaleźć błąd w kodzie.
	-Do tworzenia łańcucha służącego do kodowania/dekodowania nie można używać pętli.


---BIURA PODRÓŻY---

Każde z 3 biur podróży specjalizuje się w innym kraju(Polska, Francja, Włochy), każde z nich częściowo 
filtruje dane. Biura podróży tworzą instancje wycieczek, zdjęć i recenzji (które w żaden sposób nie są powiązane).
Wycieczka składa się z losowej liczby dni(1-4), a każdy dzień z 3 atrakcji i noclegu. 
Przygotowane dane będzie trzeba prezentować w specjalny sposób.
	-PolandTravel:
		1) Zdjęcia są pomijane jeśli długość geograficzna(Longitude) nie mieści się w przedziale [14.4,23.5],
		a szerokość(Latitude) w [49.8,54.2].
		2) Atrakcje są pomijane jeśli nie są z Polski ("Poland").
		4) Dla wiarygodności przy wyświetlaniu treści recenzji i nazw użytkowników wszystkie wystąpienia liter 
		'e','a', są zamieniane na 'ę','ą'.
		3) Dla wiarygodności przy wyświetlaniu nazw zdjęć wszystkie wystąpienia liter 's','c' są zamieniane 
		na 'ś','ć'.
	-ItalyTravel:
		1) Zdjęcia są pomijane jeśli długość geograficzna nie mieści się w przedziale [8.8,15.2],
		a szerokość w [37.7,44.0].
		2) Atrakcje są pomijane jeśli nie są z Włoch ("Italy").
		3) Dla wiarygodności przy wyświetlaniu do nazwy zdjęć dodawane jest na początku słówko "Dello_".
		4) Dla wiarygodności przy wyświetlaniu do nazwy użytkowników dodawane jest na początku słówko "Della_".
	-FranceTravel:
		1) Zdjęcia są pomijane jeśli długość geograficzna nie mieści się w przedziale [0,5.4], 
		a szerokość w [43.6,50.0].
		2) Atrakcje są pomijane jeśli nie są z Francji ("France").
		3) Dla wiarygodności przy wyświetlaniu treści recenzji słowa krótsze niż 4 znaki zamieniane 
		są na słówko "la".

Wymagania:
	-Zasady 3) 4) nie mogą być aplikowane na dane (nie można przechowywać zmienionych danych). Każdorazowo są nanoszone przy wyświetlaniu.
	-Wycieczki, zdjęcia i recenzje muszą być oddzielnymi klasami dla każdego biura podróży
	-Powinna istnieć łatwa możliwość dodania nowych biur podróży
	-Dla ułatwienia istnieje ITravelAgency, który ma być implementowany przez biura podróży. Nie może on być modyfikowany.


---AGENCJE REKLAMOWE---

Wycieczki stworzone przez biura podróży umieszczane są na stronie internetowej (Offer Website) jako oferty tworzone przez agencje reklamowe (Advertising Agency). Oferty dzielą się na graficzne i tekstowe:
	-Oferty graficzne składają się z wycieczki i kolekcji zdjęć podanych przez biuro podróży
	-Oferty tekstowe składają się z wycieczki i kolekcji recenzji podanych przez biuro podróży 
Każda z dwóch typów ofert może być stała lub tymczasowa, tzn. można ją wyświetlić tylko określoną z góry liczbę razy, później oferta wygasa (wypisywany jest komunikat "This offer is expired").

Na stronę internetową mogą trafiać oferty od różnych agencji reklamowych. Agencje reklamowe nie są powiązane z żadnym konkretnym biurem podróży, ale specjalizują się w konkretnych typach ofert. Istnieją dwa typy agencji reklamowych:
	-Jedne specjalizują się w ofertach tekstowych.
	-Drugie tworzą oferty graficzne.

Wymagania:
	-Każda agencja reklamowa tworzy oferty tylko jednego typu (graficzne lub tekstowe).
	-Każda agencja reklamowa może zamieszczać zarówno oferty tymczasowe, jak i stałe. Każda agencja reklamowa tworzy oferty z określoną liczbą zdjęć/recenzji oraz z określonym limitem wyświetleń ofert tymczasowych.
	-Agencja reklamowa do każdej oferty wykorzystuje dane dostarczane przez biuro podróży. Do każdej oferty może zostać wykorzystane inne biuro podróży, ale w ramach jednej oferty są wykorzystywane dane z jednego biura podróży.
	-Oferty mogą być tworzone niezależnie od strony internetowej. 
	-Strona internetowa ma możliwość dodania ofert zarówno zwykłych, jak i tymczasowych, korzystając z różnych agencji reklamowej i danych dostarczonych przez różne biura podróży.
	-Powinna istnieć łatwa możliwość dodania nowych agencji reklamowych.
	-Każdy typ oferty powinien być reprezentowany przez osobną klasę.


---PROGRAM---

Działanie programu polega na wielokrotnym wyświetlaniu (wypisaniu na konsoli) kolejnych stron z ofertami. W każdym obiegu pętli (pętli while w metodzie Run) należy utworzyć stronę z ofertami różnych typów (stałe i czasowe) z różnych agencji reklamowych i biur podróży, a następnie wyświetlić wszystkie oferty 3 razy aby pokazać, że niektóre oferty tymczasowe wygasły. Naciśnięcie [q] lub [Esc] powoduje zamknięcie programu, a każdy inny klawisz przechodzi do następnej strony. Strony nie są nigdzie zapisywane, tzn. nie ma możliwości powrotu do tych wyświetlonych wcześniej. 

Za "wyświetlenie" zdjęcia uznajemy wypisanie jego nazwy i rozmiaru. Wyświetlanie ma wyglądać podobnie jak w przykładzie, ale nie musi być odzwierciedlone co do spacji, bo to nie jest przedmiotem zadania. Cena wycieczki jest sumą cen atrakcji i noclegów. Ocena wycieczki jest średnią z ocen atrakcji i noclegów.

Wymagania:
	-Nie można modyfikować nic w folderze Init.
	-Nie można modyfikować funkcji main.
	-W klasie Program funkcję Run można modyfikować tylko w wyznaczonym miejscu. 



