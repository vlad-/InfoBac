
<%@ Page Title="Welcome to InfoBac, sectiunea Lectii" Language="C#" AutoEventWireup="true" CodeFile="Lectii.aspx.cs" Inherits="pages_Home" MasterPageFile="~/logIn.master" %>

<asp:Content ContentPlaceHolderID="MainContent" ID="content2" runat="server">

    <h2>
        <center class="auto-style1"><b>
        PROGRAME DE EXAMEN
        PENTRU DISCIPLINA INFORMATICĂ
      <br>
       BACALAUREAT 2013
            </b>
        </center></h2>
        <ul>
            <br>
            <li><a href="#Algoritmi"><b>Algoritmi</b></a>
            <ul>
                <li>Noţiunea de algoritm, caracteristici</li>
                <li>Date, variabile, expresii, operaţii</li>
                <li>Structuri de bază (liniară, alternativă şi repetitivă)</li>
                <li>Descrierea algoritmilor (programe pseudocod)</li>
            </ul>
                <br>
            <li><b><a href ="#Limbajul_c">Elementele de bază ale unui limbaj de programare (Pascal sau C, la alegere)</a></b>
            <ul>
                <li>Vocabularul limbajului</li>
                <li>Constante. Identificatori</li>
                <li>Noţiunea de tip de dată. Operatori aritmetici, logici, relaţionali</li>
                <li>Definirea tipurilor de date</li>
                <li>Vocabularul limbajului</li>
                <li>Variabile. Declararea variabilelor</li>
                <li>Definirea constantelor</li>
                <li>Structura programelor. Comentarii</li>
                <li>Expresii. Instrucţiunea de atribuires</li>
                <li>Citirea/scrierea datelor</li>
                <li>Structuri de control (instrucţiunea compusă, structuri alternative şi repetitive)</li>
            </ul>
                <br>
            <li><b><a href = "#Subprograme_predefinite">Subprograme predefinite</a></b>
            <ul>
                <li>Subprograme. Mecanisme de transfer prin intermediul parametrilor</li>
                <li>Proceduri şi funcţii predefinite</li>
            </ul><br>
            <li><b><a href ="#Tablouri">Tipuri structurate de date</a></b>
                <ul>
                    <li>Tipul tablou</li>
                    <li>Tipul şir de caractere<br>
                        <i>&nbsp;&nbsp;&nbsp;&nbsp;operatori, proceduri şi funcţii predefinite pentru: citire, afişare, concatenare, căutare, extragere,<br> &nbsp;&nbsp;&nbsp;&nbsp;inserare, eliminare şi conversii (şir  valoare numerică)</i>
                    </li>
                    <li>Tipul înregistrare</li>
                </ul><br />
                     <li><b><a href="#Fisiere">Fişiere text</a></b>
            <ul>
                <li>Fişiere text. Tipuri de acces</li>
                <li>Proceduri şi funcţii predefinite pentru fişiere text</li>
            </ul><br>
          <li><b><a href ="#Algoritmi_elementari">Algoritmi elementari</a></b>
            <ul>
                <li>Probleme care operează asupra cifrelor unui număr</li>
                <li>Divizibilitate. Numere prime. Algoritmul lui Euclid</li>
                <li>Şirul lui Fibonacci. Calculul unor sume cu termenul general dat</li>
                <li>Determinare minim/maxim</li>
                <li>Metode de ordonare (metoda bulelor, inserţiei, selecţiei, numărării)i</li>
                <li>Interclasare</li>
                <li>Metode de căutare (secvenţială, binară)</li>
                <li>Analiza complexităţii unui algoritm (considerând criteriile de eficienţă durata de executare şi spaţiu de memorie utilizat)</li>
            </ul>
                <br>
                 <li><b><a href ="#Subprograme_predefinite">Subprograme definite de utilizator,Proceduri şi funcţii</a></b>
            <ul>
                <li>declarare şi apel</li>
                <li>parametri formali şi parametri efectivi</li>
                <li>parametri transmişi prin valoare, parametri transmişi prin referinţă</li>
                <li>variabile globale şi variabile locale, domeniu de vizibilitate</li>
                <li>Proiectarea modulară a rezolvării unei probleme</li>
            </ul>
                <br>
            <li><b><a href ="#Recursivitate">Recursivitate</a></b>
            <ul>
                <li>Prezentare generală</li>
                <li>Proceduri şi funcţii recursive</li>
            </ul><br>

            <li><a href="#Backtracking"><b>Metoda backtracking (iterativă sau recursivă)</b></a>
            <ul>
                <li>Prezentare generală</li>
                <li>Probleme de generare. Oportunitatea utilizării metodei backtracking</li>
            </ul><br>

             <li><a href ="#Combinatorica"><b>Generarea elementelor combinatoriale)</b></a>
            <ul>
                <li>Permutări, aranjamente, combinări</li>
                <li>Produs cartezian, submulţimi</li>
            </ul><br>

             <li><a href ="#Grafuri"><b>Grafuri</b></a>
             <ul>
                 <li>Grafuri neorientate<br>
                     <ul>
                         <li>
                             <i>terminologie (nod/vârf, muchie, adiacenţă, incidenţă, grad, lanţ, lanţ elementar, ciclu, ciclu elementar, lungime, subgraf, graf parţial)</i>
                         </li>
                         <li>
                             <i>proprietăţi (conex, componentă conexă, graf complet, hamiltonian, eulerian</i>
                         </li>
                         <li>
                             <i>metode de reprezentare (matrice de adiacenţă, liste de adiacenţă)</i>
                         </li>
                     </ul>
                 </li><br />
        <li>Grafuri orientate<br>
                     <ul>
                         <li>
                             <i>terminologie (nod/vârf, arc, adiacenţă, incidenţă, grad intern şi extern, drum, drum elementar, circuit, circuit elementar, lungime, subgraf, graf parţial)</i>
                         </li>
                         <li>
                             <i>proprietăţi (tare conexitate, componentă tare conexă)</i>
                         </li>
                         <li>
                             <i>metode de reprezentare (matrice de adiacenţă, liste de adiacenţă)</i>
                         </li>
                     </ul>
                 </li><br />
     <li>Arbori<br>
                     <ul>
                         <li>
                             <i>terminologie (nod, muchie, rădăcină, descendent, descendent direct/fiu, ascendent, ascendent direct/părinte, fraţi, nod terminal, frunză)</i>
                         </li>
                         <li>
                             <i>proprietăţi (tare conexitate, componentă tare conexă)</i>
                         </li>
                         <li>
                             <i>metode de reprezentare în memorie (matrice de adiacenţă, liste ”de descendenţi”, vector ”de taţi”)</i>
                         </li>
                     </ul>
                 </li>
        </ul>
                 <br />

</ul>
                 <a id="Algoritmi"><b>Algoritmi</b></a>
                 <ul>
                     <li> <a href="http://www.ebacalaureat.ro/c/19/129/570/0/Algoritmi" target="_blank">Notiuni generale</a> </li>
                      <li> <a href="./resources/algoritmi.doc">Lectie completa</a> </li>
                 </ul>
                 </br>
                <a id="Limbajul_c"><b>Elementele de baza ale limbajului C</b></a>
                  <ul>
                      <li> <a href="./resources/Manual_Limbaj_C.doc">Manual Limbaj C</a> </li>
                 </ul>

                 <br />

                  <a id="Subprograme_predefinite"><b>Subprograme definite de utilizator</b></a>
                  <ul>
                      <li> <a href="./resources/prezentare_subprograme.doc">Prezentare subprograme C 1</a> </li>
                      <li> <a href="http://infoscience.3x.ro/c++/subprograme.htm" target="_blank">Prezentare subprograme C 2</a> </li>
                 </ul>
                 <br />
                   <a id="Tablouri"><b>Tipuri structurate de date</b></a>
                  <ul>
                      <li> <a href="http://www.cs.ucv.ro/staff/gmarian/Programare/cap6_TablouriSiruri.pdf" target="_blank">Tablouri si siruri link 1</a> </li>
                      <li> <a href="http://ro.scribd.com/doc/22328996/Tablouri-%C5%9Fi-%C5%9Firuri-de-caractere-in-C" target="_blank">Tablouri si siruri de caractere link 2</a> </li>
                        <li> <a href="http://www.tutorialeprogramare.ro/Tutorial%20C/Tipul%20inregistrare.html" target="_blank">Tipul inregistrare</a> </li>
                 </ul>
                    <br />
                   <a id="Fisiere"><b>Fisiere text</b></a>
                  <ul>
                      <li> <a href="http://ler.is.edu.ro/~cex_is/Informatica/2013/teme/9/t3.pdf" target="_blank">Fisiere text Lectie 1</a> </li>
                      <li> <a href="https://sites.google.com/site/fisierec/" target="_blank">Fisiere text Lectie 2</a> </li>
                 </ul>

                   <br />
                   <a id="Algoritmi_elementari"><b>Algoritmi elementari</b></a>
                  <ul>
                      <li> <a href="http://tutorialeplusplus.blogspot.ro/2012/01/algoritmi-algoritmul-lui-euclid-cmmdc.html" target="_blank">Algoritmul lui Euclid</a> </li>
                      <li> <a href="http://informaticasite.ro/probleme-rezolvate-c++/algoritmi-elementari/Sirul-lui-Fibonacci.html" target="_blank">Şirul lui Fibonacci</a> </li>
                       <li> <a href="http://betacode.info/c-algoritmul-pentru-determinarea-maximului-si-minumului/" target="_blank">Determinarea minimului si maximului C++</a> </li>
                       <li> <a href="http://programeinformatica.blogspot.ro/2012/10/metoda-bulelor.html" target="_blank">Metoda bulelor</a> </li>
                      <li> <a href="http://informaticasite.ro/probleme-rezolvate-c++/sortari/metoda-insertiei.html" target="_blank">Metoda insertiei</a> </li>
                      <li> <a href="http://cnamd09.wikispaces.com/file/view/Metode+de+sortare.pdf" target="_blank">Prezentarea tuturor metodelor</a> </li>
                         <li> <a href="http://www.cs.ubbcluj.ro/~per/Fp_7.pdf" target="_blank">Analiza complexitatii algoritmilor</a> </li>
                 </ul>
                    <br />
                   <a id="Recursivitate"><b>Recursivitate</b></a>
                  <ul>
                      <li> <a href="http://www.tutorialeprogramare.ro/Tutorial%20C/Recursivitate.html" target="_blank">Prezentare generala Recursivtate</a> </li>
                      <li> <a href="http://bigfoot.cs.upt.ro/~marius/curs/upc/curs15.pdf" target="_blank">Diferiti algoritmi recursivi</a> </li>
                 </ul>

        <br />
                   <a id="Backtracking"><b>Metoda Backtracking</b></a>
                  <ul>
                      <li> <a href="http://www.tutorialeprogramare.ro/Tutorial%20C/Metoda%20Backtracking.html" target="_blank">Prezentare generala a metodei</a> </li>
                      <li> <a href="./resources/backtracking.doc" target="_blank">Prezentare amanuntita a metodei</a> </li>
                      <li> <a href="https://sites.google.com/site/metodabacktracking/probleme-rezolvate" target="_blank">MEtoda Backtracking probleme rezolvate</a> </li>
                 </ul>
         <br />
                  <a id="Grafuri"><b>Grafuri</b></a>
                  <ul>
                      <li> <a href="http://89.121.249.92/2010-2011/Catedre/Informatica/11/graf1.pdf" target="_blank">Lectie completa teoria grafurilor</a> </li>
                 </ul>
         <br />
</asp:Content>
<asp:Content ID="Content3" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
        .auto-style1 {
            font-size: large;
        }
    </style>
</asp:Content>

