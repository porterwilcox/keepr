<template>
    <v-toolbar app fixed color="cyan">
        <v-btn flat href="#" icon> <!-- route to top of page -->
            <v-icon color="grey lighten-5" large>fab fa-korvue</v-icon>
        </v-btn>
        <v-toolbar-items>
            <v-text-field prepend-inner-icon="search" single-line clearable v-model="search" disabled placeholder="work in progress"></v-text-field>
        </v-toolbar-items>
        <v-toolbar-title ml-5>keepr</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-items v-if="user.id">
            <v-tooltip bottom>
                <create-keep slot="activator" />
                <span>Create a Keep</span>
            </v-tooltip>
            <v-tooltip bottom>
                <profile slot="activator" />
                <span>View Profile</span>
            </v-tooltip>
            <v-tooltip bottom>
                <v-btn slot="activator" flat fab @click="logout()">
                    <v-icon>fa-sign-out-alt</v-icon>
                </v-btn>
                <span>Logout</span>
            </v-tooltip>
        </v-toolbar-items>
        <v-toolbar-items v-else>
            <v-layout full-height align-center>
                <v-btn flat round large @click="createAccount()" class="white--text">create accout</v-btn>
            </v-layout>
        </v-toolbar-items>
    </v-toolbar>    
</template>
<script>
import CreateKeep from '@/components/CreateKeep'
import Profile from '@/components/Profile'
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
    components: {
        CreateKeep,
        Profile
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