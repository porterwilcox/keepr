<template>
    <v-toolbar fixed color="cyan lighten-3">
        <v-btn flat href="#" icon> <!-- route to top of page -->
            <v-icon color="grey lighten-5" large>fab fa-korvue</v-icon>
        </v-btn>
        <v-toolbar-items>
            <v-text-field prepend-inner-icon="search" single-line clearable v-model="search" placeholder="search keeps"></v-text-field>
        </v-toolbar-items>
        <v-toolbar-title ml-5>keeper</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-items v-if="user.id">
            <v-btn flat >
                new keep
            </v-btn>
            <v-btn flat >
                my vaults
            </v-btn>
            <v-btn icon>
                <v-icon>fas fa-user</v-icon>
            </v-btn>
            <v-btn icon @click="logout()">
                <v-icon>fa-sign-out-alt</v-icon>
            </v-btn>
        </v-toolbar-items>
        <v-toolbar-items v-else>
            <v-layout full-height align-center>
                <v-btn flat round large @click="createAccount()" class="white--text">create accout</v-btn>
            </v-layout>
        </v-toolbar-items>
    </v-toolbar>    
</template>
<script>
export default {
    name: "navigationBar",
    data(){
        return {
            search: "",
        }
    },
    computed: {
        user() {
            return this.$store.state.user
        }
    },
    methods: {
        logout()
        {
            this.$store.dispatch("logout")
        },
        createAccount() {
            this.$store.dispatch("createAccount")
        }
    }
}
</script>
<style>

</style>