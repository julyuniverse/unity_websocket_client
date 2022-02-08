## unity 클라이언트와 nodejs 서버
> 클라이언트와 서버 간에 실시간 통신을 하기 위해서 Web Socket 기술을 사용하였다.
* 2022-02-03
 * 로그인 구현
  * mysql를 통해서 로그인
* 2022-02-08
 * 로그인 구현 재조정
  * 클라이언트에서 로그인 시 필요한 id와 pw를 nodejs 서버로 던지고 nodejs 서버에서는 결괏값을 json 문자열로 클라이언트로 던져 준다.
  * nodejs 서버에서 받은 json 문자열을 파싱 하여 로그인 성공 시 ProfileScene으로 전환한다.